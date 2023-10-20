using Backend_API.Models.DTO;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IProfileRepository
    {
        Task<int> EditUserProfile(Guid userid,EditProfileDTO dto);
        Task<int> ChangeUserAvatar(Guid userid);
    }
}
