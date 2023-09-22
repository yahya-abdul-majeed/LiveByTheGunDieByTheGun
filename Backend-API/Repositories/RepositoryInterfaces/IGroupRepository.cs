using System.Text.RegularExpressions;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IGroupRepository
    {
        Task<Group> CreateGroupAsync(Group group);
        Task<Group> UpdateGroupAsync(Guid id,Group group);
        Task<bool> DeleteGroupAsync(Guid id);
        Task<Group> GetGroupByIdAsync(Guid id);
        Task<Group> GetGroupByNameAsync(int Name);
        Task<IEnumerable<Group>> GetAllGroupsAsync();
    }
}
