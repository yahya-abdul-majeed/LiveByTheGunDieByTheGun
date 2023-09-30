using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Backend_API.Models.Responses
{
    public class ReleaseJaileeResponse
    {
        public bool emailSent { get;set; }
        public string EmailJobId { get; set; } = string.Empty;
        public List<string> ErrorMessages { get; set; }
    }
}
