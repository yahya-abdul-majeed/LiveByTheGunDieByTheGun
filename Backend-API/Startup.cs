//using Backend_API.Models.UserModels;
//using Backend_API.Options;
//using Microsoft.AspNetCore.Identity;

//namespace Backend_API
//{
//    //contains methods that need to be called at application startup
//    public static class Startup
//    {
//        public static async Task CreateRolesAndPowerUser(IServiceProvider serviceProvider)
//        {
//            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//            var config = serviceProvider.GetRequiredService<IConfiguration>();
//            var powerUserOptions = config.GetSection(PowerUserOptions.PowerUser).Get<PowerUserOptions>();

//            string[] roles = { UserRoles.Admin,UserRoles.User };

//            foreach(var role in roles)
//            {
//                var roleExists = await roleManager.RoleExistsAsync(role);
//                if (!roleExists)
//                {
//                    await roleManager.CreateAsync(new IdentityRole(role));
//                }
//            }

//            var powerUser = new ApplicationUser
//            {
//                UserName = powerUserOptions.Name,
//                Email = powerUserOptions.Email,
//            };
//            var teacher = new Teacher
//            {
//                UserName = "sampleTeacher",
//                Email = "sampleteacher@gmail.com",
//                Avatar = "sample avatar teacher",
//            };
//            var student = new Student
//            {
//                UserName = "sampleStudent",
//                Email = "sampleStudent@gmail.com",
//                Avatar = "sample avatar student",
//                DirectionID = "",
//                FacultyID = "",
//                GroupID = "",
//                Grade = 3
//            };
//            var powerUserPass = powerUserOptions.Password;
//            var admin = await userManager.FindByEmailAsync(powerUserOptions.Email);
//            if(admin == null)
//            {
//                var result = await userManager.CreateAsync(powerUser,powerUserPass);
//                var teacherResult = await userManager.CreateAsync(teacher,powerUserPass);
//                var studentResult = await userManager.CreateAsync(student,powerUserPass);
//                if (result.Succeeded)
//                {
//                    await userManager.AddToRoleAsync(powerUser, UserRoles.Admin);
//                }
//            }
//        }
//    }
//}
