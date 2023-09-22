using Backend_API.Repositories.RepositoryInterfaces;
using System.Text.RegularExpressions;

namespace Backend_API.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        public Task<Group> CreateGroupAsync(Group group)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteGroupAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Group> GetGroupByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Group> GetGroupByNameAsync(int Name)
        {
            throw new NotImplementedException();
        }

        public Task<Group> UpdateGroupAsync(Guid id, Group group)
        {
            throw new NotImplementedException();
        }
    }
}
