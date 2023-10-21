
using Backend_API.Models;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IGroupRepository
    {
        Guid CreateGroup(Group group);
        Task<int> UpdateGroupAsync(Guid id,Group group);
        Task<int> DeleteGroupAsync(Guid id);
        Group GetGroupById(Guid id);
        Group GetGroupByName(string Name);
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        Task<IEnumerable<Group>> GetGroupsForDiscipline(Guid discipline_id);
    }
}
