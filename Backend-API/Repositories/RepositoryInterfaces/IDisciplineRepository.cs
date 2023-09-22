using Backend_API.Models;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IDisciplineRepository
    {
        Task<Discipline> CreateDisciplineAsync(Discipline discipline);
        Task<Discipline> UpdateDisciplineAsync(Guid id, Discipline discipline);
        Task<bool> DeleteDisciplineAsync(Guid id);
        Task<Discipline> GetDisciplineByIdAsync(Guid id);
        Task<IEnumerable<Discipline>> GetAllDisciplineAsync();

    }
}
