using Microsoft.Identity.Client;

namespace Backend_API.Models.Responses
{
    public class JwtResponse
    {
        public bool IsSuccess { get; set; }
        public string token { get; set; } = string.Empty;
        public DateTime expiration { get; set; }
        public List<string> ErrorMessages { get; set; } 
    }
}
