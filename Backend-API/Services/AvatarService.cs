using Backend_API.Services.ServiceInterfaces;

namespace Backend_API.Services
{
    public class AvatarService : IAvatarService
    {
        public Task<string> GetAvatarForUser(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
