using Backend_API.Models;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IDisciplineRepository
    {
        Task<int> CreateDisciplineAsync(Discipline discipline);
        Task<int> UpdateDisciplineAsync(Guid id, Discipline discipline);
        Task<int> DeleteDisciplineAsync(Guid id);
        Discipline GetDisciplineById(Guid id);
        Task<IEnumerable<Discipline>> GetAllDisciplinesAsync();

    }
}
