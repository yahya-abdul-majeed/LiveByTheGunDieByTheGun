using Microsoft.AspNetCore.Identity;

namespace Backend_API.Services.ServiceInterfaces
{
    public interface IUtilityService
    {
        string GenerateRandomPassword(PasswordOptions opts = null);
    }
}
