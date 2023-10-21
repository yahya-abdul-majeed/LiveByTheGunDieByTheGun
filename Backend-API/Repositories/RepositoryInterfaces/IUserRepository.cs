
using Azure.Identity;
using Backend_API.Models;
using Backend_API.Models.DTO;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IUserRepository
    {
        JailedUser GetJailedUser(Guid id);
        Guid CreateStudentUser(JailedUser jailee, string _password);
        Guid CreateTeacherUser(JailedUser jailee, string _password);

        Task<IEnumerable<ApplicationStudentDTO >> GetStudentsAsync();
        Task<IEnumerable<ApplicationTeacherDTO >> GetTeachersAsync();

        Task<int> AssignUserRoleTo(Guid userid);
        Task<int> AssignAdminRoleTo(Guid userid);

        ApplicationStudentDTO GetStudent(Guid id);
        ApplicationTeacherDTO GetTeacher(Guid id);

        Task<int> SetUserPassword(string email,  string password, byte[] token);
        Task<byte[]> SendChangePasswordEmail(string email,string link);

    }
}
