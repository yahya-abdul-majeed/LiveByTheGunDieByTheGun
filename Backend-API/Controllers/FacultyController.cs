using Backend_API.Models.Responses;
using Backend_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Backend_API.Controllers
{
    [Route("api/[controller]")]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyRepository _facultyRepository;

        public FacultyController(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        [HttpPost]
        [Route("CreateFaculty/{name}")]
        public async Task<IActionResult> CreateFaculty(string name)
        {
            var result = await _facultyRepository.CreateFacultyAsync(name);
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
                    "Faculty creation failed."
                }
            });
        }

        [HttpDelete]
        [Route("DeleteFaculty/{id}")]
        public async Task<IActionResult> DeleteFaculty(Guid id)
        {
            var result = await _facultyRepository.DeleteFacultyByIdAsync(id);
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
                    "Faculty Deletion failed."
                }
            });

        }
        [HttpGet]
        [Route("GetFaculties")]
        public async Task<IActionResult> GetFaculties()
        {
            var faculties = await _facultyRepository.GetAllFacultiesAsync();
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                    faculties
                }
            });

        }
        [HttpGet]
        [Route("GetFaculty/{id}")]
        public IActionResult GetFaculty(Guid id)
        {
            var faculty = _facultyRepository.GetFacultyById(id);
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                   faculty 
                }
            });
        }

        [HttpPut]
        [Route("UpdateFaculty")]
        public async Task<IActionResult> UpdateFaculty([FromQuery] Guid id,[FromQuery] string name)
        {
            var result = await _facultyRepository.UpdateFacultybyIdAsync(id,name);  
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
                    "Faculty Update failed."
                }
            });
        }
    }
}
