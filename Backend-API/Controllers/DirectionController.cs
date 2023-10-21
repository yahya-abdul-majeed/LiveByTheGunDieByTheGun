using Backend_API.Models;
using Backend_API.Models.Responses;
using Backend_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Backend_API.Controllers
{
    [Route("api/[controller]")]
    public class DirectionController : ControllerBase
    {
        private readonly IDirectionRepository _directionRepository;
        public DirectionController(IDirectionRepository directionRepository) {
            _directionRepository = directionRepository; 
        }

        [HttpPost]
        [Route("CreateDirection")]
        public async Task<IActionResult> CreateDirection(Direction direction)
        {
            var result = await _directionRepository.CreateDirectionAsync(direction);
            if(result > 0)
            {
                return Ok(new APIResponse
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                });
            }
            return BadRequest(new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { "Direction creation failed"}
                });
        }

        [HttpDelete]
        [Route("DeleteDirection/{id}")]
        public async Task<IActionResult> DeleteDirection(Guid id)
        {
            var result = await _directionRepository.DeleteDirectionAsync(id);
            if(result > 0)
            {
                return Ok(new APIResponse
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                });
            }
            return BadRequest(new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { "Direction delete failed"}
                });

        }

        [HttpGet]
        [Route("GetDirections")]
        public async Task<IActionResult> GetDirections()
        {
            var directions = await _directionRepository.GetAllDirectionsAsync();
            
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                   directions 
                }
            });

        }

        [HttpGet]
        [Route("GetDirection/{id}")]
        public IActionResult GetDirection(Guid id)
        {
            var direction = _directionRepository.GetDirectionById(id);
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                    direction
                }
            });

        }
        [HttpGet]
        [Route("GetDirectionWithFaculty/{id}")]
        public IActionResult GetDirectionWithFaculty(Guid id)
        {
            var direction = _directionRepository.GetDirectionWithFacutly(id);
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                    direction
                }
            });

        }


        [HttpPut]
        [Route("UpdateDirection/{id}")]
        public async Task<IActionResult> UpdateDirection([FromBody] Direction direction, [FromRoute]Guid id)
        {
            var result = await  _directionRepository.UpdateDirectionAsync(id, direction); 
            if(result >  0)
            {
                return Ok(new APIResponse
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK
                });
            }
            return BadRequest(new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new List<string>
                {
                    "Direction Update failed."
                }
            });
        }



    }
}
