using Backend_API.Models;
using Backend_API.Models.Responses;
using Backend_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Backend_API.Controllers
{
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpPost]
        [Route("CreateGroup")]
        public IActionResult CreateGroup(Group group)
        {
            try
            {
                var id = _groupRepository.CreateGroup(group);
                return Ok(new APIResponse
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Result= new
                    {
                        id
                    }
                });
            }catch(Exception ex)
            {
                var id = _groupRepository.CreateGroup(group);
                return BadRequest(new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { ex.Message }
                });

            }
        }

        [HttpPut]
        [Route("UpdateGroup/{id}")]
        public async Task<IActionResult> UpdateGroup([FromRoute]Guid id,[FromBody] Group group)
        {
            var result = await _groupRepository.UpdateGroupAsync(id, group);
            if(result >  0)
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
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                });
            }
        }

        [HttpDelete]
        [Route("DeleteGroup/{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            var result = await _groupRepository.DeleteGroupAsync(id);
            if(result >  0)
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
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                });
            }

        }
        [HttpGet]
        [Route("GetGroup/{id}")]
        public IActionResult GetGroupById(Guid id)
        {
            try
            {
                var group = _groupRepository.GetGroupById(id);
                return Ok(new APIResponse { 
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Result = new
                    {
                        group
                    }
                });

            }
            catch(Exception ex)
            {
                return BadRequest(new APIResponse
                {
                    IsSuccess= false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { ex.Message }
                });
            }
        }
        [HttpGet]
        [Route("GetGroupByName/{name}")]
        public IActionResult GetGroup(string name)
        {
            try
            {
                var group = _groupRepository.GetGroupByName(name);
                return Ok(new APIResponse { 
                    IsSuccess = true, 
                    StatusCode = HttpStatusCode.OK,
                    Result = new
                    {
                        group
                    }
                });

            }
            catch(Exception ex)
            {
                return BadRequest(new APIResponse
                {
                    IsSuccess= false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { ex.Message }
                });
            }
        }
        [HttpGet]
        [Route("GetGroups")]
        public async Task<IActionResult> GetGroups()
        {
            try
            {
                var groups = await _groupRepository.GetAllGroupsAsync();
                return Ok(new APIResponse
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Result = new
                    {
                        groups
                    }
                });

            }catch(Exception ex)
            {
                return BadRequest(new APIResponse
                {
                    IsSuccess= false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { ex.Message }
                });

            }
        }

        [HttpGet]
        [Route("GetGroupsForDiscipline/{id}")]
        public async Task<IActionResult> GetGroupsForDiscipline(Guid id)
        {
            var groups = await _groupRepository.GetGroupsForDiscipline(id);
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                    groups
                }
            });
        }


    }
}
