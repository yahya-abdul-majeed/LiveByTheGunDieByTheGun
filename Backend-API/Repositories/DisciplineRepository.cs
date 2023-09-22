using Backend_API.Models;
using Backend_API.Repositories.RepositoryInterfaces;

namespace Backend_API.Repositories
{
    public class DisciplineRepository : IDisciplineRepository
    {
        public Task<Discipline> CreateDisciplineAsync(Discipline discipline)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDisciplineAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Discipline>> GetAllDisciplineAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Discipline> GetDisciplineByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Discipline> UpdateDisciplineAsync(Guid id, Discipline discipline)
        {
            throw new NotImplementedException();
        }
    }
}
