using Backend_API.Models;
using Backend_API.Models.Responses;
using Backend_API.Repositories.RepositoryInterfaces;
using Backend_API.Services.ServiceInterfaces;
using Microsoft.Data.SqlClient;
using Hangfire;
using System.Transactions;

namespace Backend_API.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IEmailService _emailService;
        private readonly IAvatarService _avatarService;
        private readonly IUtilityService _utilityService;
        private readonly IUserRepository _userRepository;
        public AdminRepository
            (IEmailService emailService,
            IAvatarService avatarService,
            IUtilityService utilityService,
            IUserRepository userRepository)
        {
            _emailService = emailService;
            _avatarService = avatarService;
            _utilityService = utilityService;
            _userRepository = userRepository;
        }
        public async Task<ReleaseJaileeResponse> ReleaseJailedUser(JailedUser jailee)
        {
            try
            {
                Guid? id;
                string password;
                string jobId;
                TransactionManager.ImplicitDistributedTransactions = true;
                using (var transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    (id, password) = CreateUserInDatabase(jailee);
                    jobId = SendEmailToUser(jailee.email,password);
                    transactionScope.Complete();
                }

                if (id != null && jobId != null)
                {
                    return new ReleaseJaileeResponse
                    {
                        emailSent = true,
                        EmailJobId = jobId,
                    };
                }
                else
                {
                    return new ReleaseJaileeResponse
                    {
                        emailSent = false,
                        ErrorMessages = new List<string>
                    {
                        "User creation failed"
                    }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ReleaseJaileeResponse
                {
                    emailSent = false,
                    ErrorMessages = new List<string>
                    {
                       ex.Message,
                       ex.StackTrace
                    }
                };
            }
        }
        private (Guid?,string) CreateUserInDatabase(JailedUser jailee)
        {
            var password = _utilityService.GenerateRandomPassword();
            if(jailee.is_student)
            {
                return (_userRepository.CreateStudentUser(jailee,password),password); 
            }
            return (_userRepository.CreateTeacherUser(jailee, password), password);

        }
        private string SendEmailToUser(string email, string password)
        {
            return BackgroundJob.Enqueue(() => _emailService.SendEmailWithPassword(email, password));
        }


    }
}
