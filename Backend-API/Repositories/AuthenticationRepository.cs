using Backend_API.Models;
using Backend_API.Models.DTO;
using Backend_API.Models.Responses;
using Backend_API.Options;
using Backend_API.Repositories.RepositoryInterfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend_API.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly JWTOptions _options;
        private readonly string _connectionString;

        public AuthenticationRepository(
            IOptionsSnapshot<JWTOptions> options,
            IConfiguration configuration)
        {
            _options = options.Value;
            _connectionString = configuration.GetConnectionString("SQLConnection");
        }
        public async Task<JwtResponse> Login(LoginDTO dto)
        {
            try
            {

                if (!UserExists(dto.email))
                {
                    return new JwtResponse
                    {
                        IsSuccess = false,
                        token = string.Empty,
                        ErrorMessages = new List<string>()
                        {
                            "User with this email not found"
                        }
                    };
                }
                if (CheckUserPassword(dto.email,dto.password))
                {
                    applicationuser user = LoadUser(dto.email);
                    IEnumerable<string> userRoles = await GetUserRolesAsync(user.user_id);
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var token = GetToken(authClaims);
                    return new JwtResponse
                    {
                        IsSuccess = true,
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        ErrorMessages = new List<string>()
                    };

                }
                else
                {

                    return new JwtResponse
                    {
                        IsSuccess = false,
                        token = string.Empty,
                        ErrorMessages = new List<string>()
                        {
                            "Password incorrect"
                        }
                    };

                }
            }
            catch (Exception ex)
            {
                return new JwtResponse
                {
                    IsSuccess = false,
                    token = string.Empty,
                    ErrorMessages = new List<string>()
                    {
                        ex.Message,
                    }
                };
            }
        }

      
        public async Task<RegistrationResponse> Register(RegistrationDTO dto)
        {
            try
            {
                if (UserExists(dto.email))
                {
                    return new RegistrationResponse
                    {
                        IsSuccess = false,
                        ErrorMessages = new List<string>()
                        {
                            "User with this email already exists"
                        }
                    };
                }
                JailedUser jailedUser = new()
                {
                    username = dto.username,
                    email = dto.email,
                    phone= dto.phone,
                    birthdate = dto.birthdate,
                    is_student = dto.is_student,
                    avatar = dto.avatar,
                    grade_id = dto.grade_id,
                    direction_id = dto.direction_id,
                    faculty_id = dto.faculty_id,
                    group_id = dto.group_id
                };

                var result = await CreateJailedUserAsync(jailedUser);
                if(result > 0)
                {
                    return new RegistrationResponse
                    {
                        IsSuccess = true,
                        ErrorMessages = new List<string>()
                    };
                }

                return new RegistrationResponse
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string>()
                    {
                        "Failed"
                    }
                };

            }
            catch (Exception ex)
            {
                return new RegistrationResponse
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string>()
                    {
                        ex.Message,
                    }
                };
            }
        }

        private bool UserExists(string email)
        {
            using (var connection = new SqlConnection(_connectionString)) {
                var sql = @"
                        SELECT CASE WHEN EXISTS(
                            SELECT * FROM management.applicationuser
                            WHERE email = @email
                        )
                        THEN CAST(1 AS BIT)
                        ELSE CAST(0 AS BIT) END AS user_exists
                        ";
                return connection.QuerySingleOrDefault<bool>(sql, new {email});
            }
        }
        private async Task<IEnumerable<string>> GetUserRolesAsync(Guid user_id)
        {
            using (var connection = new SqlConnection(_connectionString)) {
                var sql = @"
                        SELECT role_name FROM management.a_user_approle a
                        LEFT JOIN management.approle b
                        ON a.role_id = b.role_id
                        WHERE a.user_id = @user_id
                        ";
                return await connection.QueryAsync<string>(sql, new {user_id});
            }
        }

        private applicationuser LoadUser(string email)
        {
            using (var connection = new SqlConnection(_connectionString)) {
                var sql = @"
                        SELECT * FROM management.applicationuser
                        WHERE email = @email
                        ";
                return connection.QuerySingleOrDefault<applicationuser>(sql, new {email});
            }
        }

        private bool CheckUserPassword(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString)) {
                var sql = @"
                    SELECT CASE WHEN EXISTS(
                        SELECT * FROM management.applicationuser
                        WHERE password = @password AND email = @email
                    )
                    THEN CAST(1 AS BIT)
                    ELSE CAST(0 AS BIT) END
                        ";
                return connection.QuerySingleOrDefault<bool>(sql, new {email, password});
            }
        }

        private async Task<int> CreateJailedUserAsync(JailedUser jailee)
        {
            using(var connection = new SqlConnection(_connectionString)) {
                var (name, _birthdate, _email, _phone, _avatar, _is_student, _faculty_id, _direction_id, _group_id, _grade_id) = jailee;
                string sql;
                if (_is_student)
                {
                    sql = @"
                             INSERT INTO management.jaileduser
                             (username,birthdate,email,phone,avatar,is_student,faculty_id,direction_id,group_id,grade_id)
                             VALUES (@name,@_birthdate,@_email,@_phone,@_avatar,@_is_student,@_faculty_id,@_direction_id,@_group_id,@_grade_id)
                            "; 
                }
                else
                {
                    sql = @"
                             INSERT INTO management.jaileduser
                             (username,birthdate,email,phone,avatar,is_student)
                             VALUES (@name,@_birthdate,@_email,@_phone,@_avatar,@_is_student)
                            ";
                }
                return await connection.ExecuteAsync(sql, new {name, _birthdate, _email, _phone,_avatar,_is_student,_faculty_id,_direction_id,_group_id,_grade_id});
            }
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));

            var token = new JwtSecurityToken(
                issuer: _options.ValidIssuer,
                audience: _options.ValidAudience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
