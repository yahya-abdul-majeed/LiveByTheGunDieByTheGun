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
        public IActionResult CreateTeacher(JailedUser jailee, string password)
        {
            var userid = _userRepository.CreateTeacherUser(jailee, password);
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
        public IActionResult CreateStudent(JailedUser jailee, string password)
        {
            var userid = _userRepository.CreateStudentUser(jailee, password);
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

        [HttpPost]
        [Route("SendChangePasswordLink/{email}")]
        public async Task<IActionResult> SendChangePasswordLink(string email,string link)
        {
            var token = await _userRepository.SendChangePasswordEmail(email,link);
            return Ok(new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = new
                {
                    token
                }
            });
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string email,string password, byte[] token)
        {
            var result = await _userRepository.SetUserPassword(email,password,token);
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
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string>
                    {
                        "Password couldn't be changed. Link might have expired. Request a new link.",
                        "Enter a stronger password with Capital letter, digit and special character",
                        "User with such email might not exist"
                    }
                });
            }
        }

    }
}
