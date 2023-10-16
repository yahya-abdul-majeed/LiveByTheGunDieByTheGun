using Backend_API.Models.Responses;
using Backend_API.Services.ServiceInterfaces;
using Hangfire;

namespace Backend_API.Services
{
    public class EmailService : IEmailService
    {
        //[AutomaticRetry(Attempts = 15)]
        public void SendEmailWithPassword(string email, string password)
        {
            throw new Exception();
        }
    }
}
