using Backend_API.Models.DTO;
using Backend_API.Repositories.RepositoryInterfaces;

namespace Backend_API.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        public Task<bool> Login(LoginDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(RegistrationDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
