using Backend_API.Data;
using Backend_API.Models.UserModels;
using Backend_API.Repositories.RepositoryInterfaces;

namespace Backend_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            this._context = context;
        }


        public async Task<JailedUser> GetJailedUserAsync(Guid id)
        {
            var jailee = await _context.JailedUsers.FindAsync(id);
            return jailee;
        }
    }
}
