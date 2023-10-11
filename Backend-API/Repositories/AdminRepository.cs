//using Backend_API.Models.Responses;
//using Backend_API.Models.UserModels;
//using Backend_API.Repositories.RepositoryInterfaces;
//using Backend_API.Services.ServiceInterfaces;
//using Hangfire;
//using Microsoft.AspNetCore.Identity;

//namespace Backend_API.Repositories
//{
//    public class AdminRepository : IAdminRepository
//    {
//        private readonly IEmailService _emailService;
//        private readonly IAvatarService _avatarService;
//        private readonly UserManager<ApplicationUser> _userManager;
//        public AdminRepository
//            (IEmailService emailService,
//            IAvatarService avatarService,
//            UserManager<ApplicationUser> userManager)
//        {
//            _emailService = emailService;
//            _avatarService = avatarService;
//            _userManager = userManager;

//        }
//        public async Task<ReleaseJaileeResponse> ReleaseJailedUser(JailedUser jailee)
//        {
//            try
//            {
//                ApplicationUser user;
//                if(jailee.GroupID != null && jailee.FacultyID != null && jailee.DirectionID != null && jailee.Grade != null)
//                {
//                    user = new Student()
//                    {
//                        UserName = jailee.Name,
//                        Email = jailee.Email,
//                        PhoneNumber = jailee.Phone,
//                        BirthDate = jailee.BirthDate,
//                        Avatar = await _avatarService.GetDefaultAvatar(jailee.Id),
//                        FacultyID = jailee.FacultyID,
//                        DirectionID = jailee.DirectionID,
//                        GroupID = jailee.GroupID,
//                        Grade = (int)jailee.Grade
//                    };
//                }
//                else
//                {
//                    user = new Teacher()
//                    {
//                        UserName = jailee.Name,
//                        Email = jailee.Email,
//                        PhoneNumber = jailee.Phone,
//                        BirthDate = jailee.BirthDate,
//                        Avatar = await _avatarService.GetDefaultAvatar(jailee.Id),
//                    };
//                }

//                //create roles before assiging users to them
//                //await _userManager.AddToRoleAsync(user, UserRoles.User);

//                //var generatedPassword = GenerateRandomPassword();
//                var generatedPassword = "Admin123_";
//                var result = await _userManager.CreateAsync(user, generatedPassword);
//                if(result.Succeeded)
//                {
//                    var jobId = BackgroundJob.Enqueue(() => _emailService.SendEmailWithPassword(jailee.Email,generatedPassword));
//                    return new ReleaseJaileeResponse
//                    {
//                        emailSent = true,
//                        EmailJobId = jobId,
//                    };
//                }
//                else
//                {
//                    return new ReleaseJaileeResponse
//                    {
//                        emailSent = false,
//                        ErrorMessages = new List<string>
//                        {
//                            "User creation failed"
//                        }
//                    };
//                }
//            }
//            catch(Exception ex)
//            {
//                return new ReleaseJaileeResponse
//                {
//                    emailSent = false,
//                    ErrorMessages = new List<string>
//                    {
//                       ex.Message,
//                       ex.StackTrace
//                    }
//                };
//            }
//        }

//        private string GenerateRandomPassword(PasswordOptions opts = null)
//        {
//            if (opts == null) opts = new PasswordOptions()
//            {
//                RequiredLength = 8,
//                RequiredUniqueChars = 4,
//                RequireDigit = true,
//                RequireLowercase = true,
//                RequireNonAlphanumeric = true,
//                RequireUppercase = true
//            };

//            string[] randomChars = new[] {
//                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
//                "abcdefghijkmnopqrstuvwxyz",    // lowercase
//                "0123456789",                   // digits
//                "!@$?_-"                        // non-alphanumeric
//            };

//            Random rand = new Random(Environment.TickCount);
//            List<char> chars = new List<char>();

//            if (opts.RequireUppercase)
//                chars.Insert(rand.Next(0, chars.Count), 
//                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

//            if (opts.RequireLowercase)
//                chars.Insert(rand.Next(0, chars.Count), 
//                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

//            if (opts.RequireDigit)
//                chars.Insert(rand.Next(0, chars.Count), 
//                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

//            if (opts.RequireNonAlphanumeric)
//                chars.Insert(rand.Next(0, chars.Count), 
//                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

//            for (int i = chars.Count; i < opts.RequiredLength
//                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
//            {
//                string rcs = randomChars[rand.Next(0, randomChars.Length)];
//                chars.Insert(rand.Next(0, chars.Count), 
//                    rcs[rand.Next(0, rcs.Length)]);
//            }

//            return new string(chars.ToArray());
//        }

//    }
//}
