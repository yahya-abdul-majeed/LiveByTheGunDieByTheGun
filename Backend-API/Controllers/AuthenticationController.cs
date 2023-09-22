using Backend_API.Models.DTO;
using Backend_API.Models.Responses;
using Backend_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<APIResponse>> Register(RegistrationDTO dto)
        {
            var result = await _authenticationRepository.Register(dto);
            return new APIResponse
            {

            };
        } 

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<APIResponse>> Login(LoginDTO dto)
        {
            var result = await _authenticationRepository.Login(dto);
            return new APIResponse
            {

            };

        }
    }
}
