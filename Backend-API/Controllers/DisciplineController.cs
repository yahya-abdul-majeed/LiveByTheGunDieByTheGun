using Backend_API.Models;
using Backend_API.Models.Responses;
using Backend_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Backend_API.Controllers
{
    [Route("api/[controller]")]
    public class DisciplineController : ControllerBase
    {
        private readonly IDisciplineRepository _disciplineRepository;
        public DisciplineController(IDisciplineRepository disciplineRepository)
        {
            _disciplineRepository = disciplineRepository; 
        }

        [HttpPost]
        [Route("CreateDiscipline")]
        public async Task<IActionResult> CreateDiscipline(Discipline discipline)
        {
            var result = await _disciplineRepository.CreateDisciplineAsync(discipline);
            if(result > 0)
            {
                return Ok(new APIResponse { 
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                });
            }
            return BadRequest(new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new List<string>
                {
                    "Discipline creation failed"
                }
            });

        }

        [HttpDelete]
        [Route("DeleteDiscipline/{id}")]
        public async Task<IActionResult> DeleteDiscipline(Guid id)
        {
            var result = await _disciplineRepository.DeleteDisciplineAsync(id);
            if(result > 0)
            {
                return Ok(new APIResponse { 
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                });
            }
            return BadRequest(new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new List<string>
                {
                    "Discipline Deletion failed"
                }
            });
        }

        [HttpGet]
        [Route("GetDisciplines")]
        public async Task<IActionResult> GetDisciplines()
        {
            var disciplines = await _disciplineRepository.GetAllDisciplinesAsync();
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                    disciplines
                }
            });
        }
        [HttpGet]
        [Route("GetDisciplinesForTeacher/{id}")]
        public async Task<IActionResult> GetDisciplinesForTeacher(Guid id)
        {
            var disciplines = await _disciplineRepository.GetAllDisciplinesForTeacherAsync(id);
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                    disciplines
                }
            });
        }
        [HttpGet]
        [Route("GetTeachersForDiscipline/{id}")]
        public async Task<IActionResult> GetTeachersForDiscipline(Guid id)
        {
            var teachers = await _disciplineRepository.GetAllTeachersForDisciplineAsync(id);
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                   teachers 
                }
            });
        }

        [HttpGet]
        [Route("GetDiscipline/{id}")]
        public IActionResult GetDiscipline(Guid id)
        {
            var discipline = _disciplineRepository.GetDisciplineById(id);
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                    discipline
                }
            });
        }

        [HttpPut]
        [Route("UpdateDiscipline/{id}")]
        public async Task<IActionResult> UpdateDiscipline([FromRoute]Guid id, [FromBody]Discipline discipline)
        {
            var result = await _disciplineRepository.UpdateDisciplineAsync(id, discipline);
            if(result > 0)
            {
                return Ok(new APIResponse { 
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                });
            }
            return BadRequest(new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new List<string>
                {
                    "Discipline Deletion failed"
                }
            });
        }
    }
}
