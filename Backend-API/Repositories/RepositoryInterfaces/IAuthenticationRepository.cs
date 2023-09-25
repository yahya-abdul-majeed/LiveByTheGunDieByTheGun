using Backend_API.Models.DTO;
using Backend_API.Models.Responses;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IAuthenticationRepository
    {
        Task<RegistrationResponse> Register(RegistrationDTO dto);
        Task<JwtResponse> Login(LoginDTO dto);
    }
}
