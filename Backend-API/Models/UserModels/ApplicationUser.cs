using Microsoft.AspNetCore.Identity;

namespace Backend_API.Models.UserModels
{
    public class ApplicationUser : IdentityUser
    {
        public DateOnly BirthDate { get; set; }
        public string Avatar { get; set; } = string.Empty;

    }
}
