using Backend_API.Models.UserModels;

namespace Backend_API.Services.ServiceInterfaces
{
    public interface IAvatarService
    {
        Task<string> GetDefaultAvatar(Guid userId);
        Task<string> GetCustomAvatar();
    }
}
