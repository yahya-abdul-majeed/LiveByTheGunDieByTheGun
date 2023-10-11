//using Backend_API.Data;
//using Backend_API.Models.DTO;
//using Backend_API.Models.Responses;
//using Backend_API.Models.UserModels;
//using Backend_API.Options;
//using Backend_API.Repositories.RepositoryInterfaces;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using System.Diagnostics;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace Backend_API.Repositories
//{
//    public class AuthenticationRepository : IAuthenticationRepository
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly ApplicationDbContext _context;
//        private readonly IConfiguration _configuration;
//        private readonly JWTOptions _options;

//        public AuthenticationRepository(
//            UserManager<ApplicationUser> userManager,
//            ApplicationDbContext context,
//            IOptionsSnapshot<JWTOptions> options,
//            IConfiguration configuration)
//        {
//            _userManager = userManager;
//            _context = context;
//            _configuration = configuration;
//            _options = options.Value;
//        }
//        public async Task<JwtResponse> Login(LoginDTO dto)
//        {
//            try
//            {

//                var user = await _userManager.FindByEmailAsync(dto.Email);
//                if(user == null)
//                {
//                    return new JwtResponse
//                    {
//                        IsSuccess = false,
//                        token = string.Empty, 
//                        ErrorMessages = new List<string>()
//                        {
//                            "User with this email not found"
//                        }
//                    };
//                }
//                if(await _userManager.CheckPasswordAsync(user, dto.Password))
//                {
//                    var userRoles = await _userManager.GetRolesAsync(user);
//                    var authClaims = new List<Claim>
//                    {
//                        new Claim(ClaimTypes.Name, user.UserName),
//                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//                    };

//                    foreach(var role in userRoles)
//                    {
//                        authClaims.Add(new Claim(ClaimTypes.Role, role));
//                    }

//                    var token = GetToken(authClaims);
//                    return new JwtResponse
//                    {
//                        IsSuccess = true,
//                        token = new JwtSecurityTokenHandler().WriteToken(token),
//                        expiration = token.ValidTo,
//                        ErrorMessages = new List<string>()
//                    };

//                }
//                else
//                {
                    
//                    return new JwtResponse
//                    {
//                        IsSuccess = false,
//                        token = string.Empty, 
//                        ErrorMessages = new List<string>()
//                        {
//                            "Password incorrect"
//                        }
//                    };

//                }
//            }
//            catch(Exception ex)
//            {
//                return new JwtResponse
//                {
//                    IsSuccess = false,
//                    token = string.Empty, 
//                    ErrorMessages = new List<string>()
//                    {
//                        ex.Message,
//                        ex.StackTrace
//                    }
//                };
//            }
//        }


//        public async Task<RegistrationResponse> Register(RegistrationDTO dto)
//        {
//            try
//            {
//                var userExists = await _userManager.FindByEmailAsync(dto.Email); 
//                if(userExists != null)
//                {
//                    return new RegistrationResponse {
//                        IsSuccess = false,
//                        ErrorMessages = new List<string>()
//                        {
//                            "User with this email already exists"
//                        }
//                    };
//                }
//                JailedUser jailedUser = new()
//                {
//                    Name = dto.Name,
//                    Email = dto.Email,
//                    Phone = dto.Phone,
//                    BirthDate = dto.BirthDate,
//                    Grade = dto.Grade,
//                    DirectionID = dto.DirectionID,
//                    FacultyID = dto.FacultyID,
//                    GroupID = dto.GroupID
//                };
                
//                _context.Add<JailedUser>(jailedUser);
//                await _context.SaveChangesAsync();
//                return new RegistrationResponse{ 
//                    IsSuccess = true,
//                    ErrorMessages = new List<string>()
//                };

//            }
//            catch(Exception ex)
//            {
//                return new RegistrationResponse
//                {
//                    IsSuccess = false,
//                    ErrorMessages = new List<string>()
//                    {
//                        ex.Message,
//                        ex.StackTrace
//                    }
//                };
//            }
//        }

//        private JwtSecurityToken GetToken(List<Claim> authClaims)
//        {
//            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));

//            var token = new JwtSecurityToken(
//                issuer: _options.ValidIssuer,
//                audience: _options.ValidAudience,
//                expires: DateTime.Now.AddHours(3),
//                claims: authClaims,
//                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
//                );

//            return token;
//        }
//    }
//}
