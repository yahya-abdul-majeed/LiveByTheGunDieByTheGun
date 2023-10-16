using Backend_API.Models;
using Backend_API.Models.DTO;
using Backend_API.Models.Responses;
using Backend_API.Options;
using Backend_API.Repositories.RepositoryInterfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend_API.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly JWTOptions _options;
        private readonly string _connectionString;

        public AuthenticationRepository(
            IOptionsSnapshot<JWTOptions> options,
            IConfiguration configuration)
        {
            _configuration = configuration;
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
                    IList<string> userRoles = GetUserRoles(user.user_id);
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
                        ex.StackTrace
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

                var result = CreateJailedUser(jailedUser);
                if(result > 1)
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
            throw new NotImplementedException();
        }
        private IList<string> GetUserRoles(Guid user_id)
        {
            throw new NotImplementedException();
        }

        private applicationuser LoadUser(string email)
        {
            throw new NotImplementedException();
        }

        private bool CheckUserPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        private int CreateJailedUser(JailedUser jailee)
        {
            throw new NotImplementedException();
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
