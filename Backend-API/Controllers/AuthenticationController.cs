using Backend_API.ModelBinders;
using Backend_API.Models.DTO;
using Backend_API.Models.Responses;
using Backend_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Backend_API.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController: ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        /// <summary>
        /// Sends a registration request to admin.
        /// </summary>
        ///<remarks>Caution: name must not contain space, only letters and numbers, set student only fields to null when creating teacher</remarks>
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<APIResponse>> Register([FromBody]RegistrationDTO dto)
        {
            var result = await _authenticationRepository.Register(dto);

            if (result.IsSuccess)
            {
                return Ok(new APIResponse
                {
                    IsSuccess = result.IsSuccess,
                    ErrorMessages = result.ErrorMessages,
                    StatusCode = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError
                });
            }
            else
            {
                return BadRequest(new APIResponse
                {
                    IsSuccess = result.IsSuccess,
                    ErrorMessages = result.ErrorMessages,
                    StatusCode = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError
                });
            }
            
        } 

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<APIResponse>> Login(LoginDTO dto)
        {
            var result = await _authenticationRepository.Login(dto);
            return new APIResponse
            {
                IsSuccess = result.IsSuccess,
                ErrorMessages = result.ErrorMessages,
                Result = new
                {
                    token = result.token,
                    expiration = result.expiration
                },
                StatusCode = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError
            };

        }
    }
}
