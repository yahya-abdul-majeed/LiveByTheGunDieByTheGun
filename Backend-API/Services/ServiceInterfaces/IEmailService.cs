using Backend_API.Models.Responses;

namespace Backend_API.Services.ServiceInterfaces
{
    public interface IEmailService
    {
        void SendEmailWithPassword(string email, string password);
        void SendResetPasswordEmail(string email,string link);
    }
}
