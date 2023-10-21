using Backend_API.Models;
using Backend_API.Models.DTO;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IDisciplineRepository
    {
        Task<int> CreateDisciplineAsync(Discipline discipline);
        Task<int> UpdateDisciplineAsync(Guid id, Discipline discipline);
        Task<int> DeleteDisciplineAsync(Guid id);
        Discipline GetDisciplineById(Guid id);
        Task<IEnumerable<Discipline>> GetAllDisciplinesAsync();
        Task<IEnumerable<Discipline>> GetAllDisciplinesForTeacherAsync(Guid userid);
        Task<IEnumerable<ApplicationTeacherDTO>> GetAllTeachersForDisciplineAsync(Guid discipline_id);

    }
}
