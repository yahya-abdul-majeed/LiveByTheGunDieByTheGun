using Backend_API.Models.DTO;
using Backend_API.Models.Responses;
using Backend_API.Repositories;
using Backend_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Backend_API.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        [HttpPut]
        [Route("UpdateAvatar/{id}")]
        public async Task<IActionResult> UpdateAvatar([FromRoute]Guid id)
        {
            try
            {
                var result = await _profileRepository.ChangeUserAvatar(id);
                if(result > 0)
                {
                    return Ok(new APIResponse
                    {
                        IsSuccess = true,
                        StatusCode = HttpStatusCode.OK,
                    });
                }
                else
                {
                    return BadRequest(new APIResponse
                    {
                        IsSuccess =false,
                        StatusCode = HttpStatusCode.BadRequest,
                    });
                }

            }catch(Exception ex) {
            
                    return BadRequest(new APIResponse
                    {
                        IsSuccess =false,
                        StatusCode = HttpStatusCode.BadRequest,
                        ErrorMessages = new List<string>()
                        {
                            ex.Message
                        }
                    });
            }
        }

        [HttpPut]
        [Route("UpdateInfo/{id}")]
        public async Task<IActionResult> UpdateInfo([FromRoute]Guid id, [FromBody]EditProfileDTO dto)
        {
            try
            {
                var result = await _profileRepository.EditUserProfile(id,dto);
                if(result > 0)
                {
                    return Ok(new APIResponse
                    {
                        IsSuccess = true,
                        StatusCode = HttpStatusCode.OK,
                    });
                }
                else
                {
                    return BadRequest(new APIResponse
                    {
                        IsSuccess =false,
                        StatusCode = HttpStatusCode.BadRequest,
                    });
                }

            }catch(Exception ex) {
            
                    return BadRequest(new APIResponse
                    {
                        IsSuccess =false,
                        StatusCode = HttpStatusCode.BadRequest,
                        ErrorMessages = new List<string>()
                        {
                            ex.Message
                        }
                    });
            }
        }

        
    }
}
