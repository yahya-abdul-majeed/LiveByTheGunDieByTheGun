using Backend_API.Models.Responses;

namespace Backend_API.Services.ServiceInterfaces
{
    public interface IEmailService
    {
        Task<EmailResponse> SendEmailWithPassword(string email, string password);
    }
}
