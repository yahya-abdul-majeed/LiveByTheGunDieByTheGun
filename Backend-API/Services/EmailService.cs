using Backend_API.Models.Responses;
using Backend_API.Services.ServiceInterfaces;

namespace Backend_API.Services
{
    public class EmailService : IEmailService
    {
        public Task<EmailResponse> SendEmailWithPassword(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
