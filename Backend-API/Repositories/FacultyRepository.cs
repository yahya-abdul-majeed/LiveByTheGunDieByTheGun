using Backend_API.Models;
using Backend_API.Repositories.RepositoryInterfaces;

namespace Backend_API.Repositories
{
    public class FacultyRepository : IFacultyRepository
    {
        public Task<Faculty> CreateFacultyAsync(string facultyName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFacultyByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Faculty>> GetAllFacultiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Faculty> GetFacultyByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Faculty> UpdateFacultybyIdAsync(Guid id, Faculty faculty)
        {
            throw new NotImplementedException();
        }
    }
}
