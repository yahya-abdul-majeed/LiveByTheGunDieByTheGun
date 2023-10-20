using Backend_API.Models;
using Backend_API.Models.Responses;
using Backend_API.Repositories.RepositoryInterfaces;
using Backend_API.Services.ServiceInterfaces;
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
                using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,TransactionScopeAsyncFlowOption.Enabled))
                {
                    (id, password) = await CreateUserInDatabase(jailee);
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
        private async Task<(Guid?,string)> CreateUserInDatabase(JailedUser jailee)
        {
            Guid userId;
            var password = _utilityService.GenerateRandomPassword();
            jailee.avatar = await _avatarService.GetDefaultAvatar(jailee.user_id);
            if(jailee.is_student)
            {
                userId = _userRepository.CreateStudentUser(jailee,password); 
            }
            userId = _userRepository.CreateTeacherUser(jailee, password);
            await _userRepository.AssignUserRoleTo(userId);
            return (userId, password);

        }
        private string SendEmailToUser(string email, string password)
        {
            return BackgroundJob.Enqueue(() => _emailService.SendEmailWithPassword(email, password));
        }


    }
}
