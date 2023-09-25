using Backend_API.Models.Responses;
using Backend_API.Models.UserModels;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IAdminRepository
    {
        Task<ReleaseJaileeResponse> ReleaseJailedUser(JailedUser jailee);
    }
}
