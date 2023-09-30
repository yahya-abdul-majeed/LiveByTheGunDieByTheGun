using Backend_API.Refit;
using Backend_API.Services.ServiceInterfaces;

namespace Backend_API.Services
{
    public class AvatarService : IAvatarService
    {
        private readonly IAvatarClient _avatarClient;

        public AvatarService(IAvatarClient avatarClient) => 
            this._avatarClient = avatarClient;
        public Task<string> GetDefaultAvatar(Guid userId)
        {
            return _avatarClient.GetAvatar(userId);
        }

        public Task<string> GetCustomAvatar()
        {
            throw new NotImplementedException();
        }
    }
}
