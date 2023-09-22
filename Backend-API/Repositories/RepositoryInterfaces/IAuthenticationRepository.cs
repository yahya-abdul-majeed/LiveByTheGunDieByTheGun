using Backend_API.Models.DTO;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IAuthenticationRepository
    {
        Task<bool> Register(RegistrationDTO dto);
        Task<bool> Login(LoginDTO dto);
    }
}
