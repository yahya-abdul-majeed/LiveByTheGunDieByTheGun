
using Azure.Identity;
using Backend_API.Models;
using Backend_API.Models.DTO;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IUserRepository
    {
        JailedUser GetJailedUser(Guid id);
        Guid CreateStudentUser(JailedUser jailee);
        Guid CreateTeacherUser(JailedUser jailee);

        Task<IEnumerable<ApplicationStudentDTO >> GetStudentsAsync();
        Task<IEnumerable<ApplicationTeacherDTO >> GetTeachersAsync();

        ApplicationStudentDTO GetStudent(Guid id);
        ApplicationTeacherDTO GetTeacher(Guid id);
    }
}
