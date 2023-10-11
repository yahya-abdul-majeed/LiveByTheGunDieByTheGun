using Backend_API.Models;
using Backend_API.Models.Responses;
using Backend_API.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Backend_API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("CreateTeacher")]
        public IActionResult CreateTeacher(JailedUser jailee)
        {
            var userid = _userRepository.CreateTeacherUser(jailee);
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                   userid 
                }
            });
        }

        [HttpPost]
        [Route("CreateStudent")]
        public IActionResult CreateStudent(JailedUser jailee)
        {
            var userid = _userRepository.CreateStudentUser(jailee);
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                   userid 
                }
            });
        }

        [HttpGet]
        [Route("GetStudent/{id}")]
        public IActionResult GetStudent(Guid id)
        {
            var student = _userRepository.GetStudent(id);
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                    student
                }
            });
        }

        [HttpGet]
        [Route("GetTeacher/{id}")]
        public IActionResult GetTeacher(Guid id)
        {
            var teacher = _userRepository.GetTeacher(id);
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                   teacher 
                }
            });
        }

        [HttpGet]
        [Route("GetTeachers")]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _userRepository.GetTeachersAsync();
            return Ok(new APIResponse { 
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                    teachers
                }
            });
        }
        [HttpGet]
        [Route("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _userRepository.GetStudentsAsync();
            return Ok(new APIResponse { 
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                   students 
                }
            });
        }

    }
}
