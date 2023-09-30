using Refit;

namespace Backend_API.Refit
{
    public interface IAvatarClient
    {
        [Get("")]
        Task<string> GetAvatar(Guid seed);
    }
}
