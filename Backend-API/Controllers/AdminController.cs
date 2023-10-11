//using Backend_API.Models.Responses;
//using Backend_API.Repositories.RepositoryInterfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;

//namespace Backend_API.Controllers
//{
//    //[Authorize(Roles = UserRoles.Admin)]
//    public class AdminController: ControllerBase
//    {
//        private readonly IAdminRepository _adminRepository;
//        private readonly IUserRepository _userRepository;

//        public AdminController(IAdminRepository adminRepository, IUserRepository userRepository)
//        {
//            this._adminRepository = adminRepository;
//            this._userRepository = userRepository;
//        }
//        /// <summary>
//        /// Accepts user request for registration, and send email to them with password.
//        /// </summary>
//        [HttpPost]
//        [Route("/release/{id}")]
//        public async Task<ActionResult<APIResponse>> ReleaseJailee(Guid id)
//        {
//            var jailee = await _userRepository.GetJailedUserAsync(id);
//            if (jailee == null)
//                return new APIResponse
//                {
//                    ErrorMessages = new List<string> { "User with such id not found" },
//                    StatusCode = HttpStatusCode.BadRequest,
//                    IsSuccess = false
//                };

//            var result = await _adminRepository.ReleaseJailedUser(jailee);
//            return new APIResponse
//            {
//                ErrorMessages = result.ErrorMessages,
//                IsSuccess = result.emailSent,
//                StatusCode = result.emailSent ? HttpStatusCode.OK : HttpStatusCode.InternalServerError,
//                Result = new
//                {
//                    emailSent = result.emailSent
//                }
//            };
//        }
//    }
//}
