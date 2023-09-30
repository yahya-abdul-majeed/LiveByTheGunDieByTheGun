using Backend_API.Models.UserModels;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<JailedUser> GetJailedUserAsync(Guid id);
    }
}
