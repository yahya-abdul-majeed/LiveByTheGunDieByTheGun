using Backend_API.Models;
using Backend_API.Models.Responses;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IAdminRepository
    {
        Task<ReleaseJaileeResponse> ReleaseJailedUser(JailedUser jailee);
    }
}
