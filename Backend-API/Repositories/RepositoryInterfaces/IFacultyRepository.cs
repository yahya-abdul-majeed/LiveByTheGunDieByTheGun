using Backend_API.Models;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IFacultyRepository
    {
        Task<Faculty> CreateFacultyAsync(string facultyName); 
        Task<Faculty> UpdateFacultybyIdAsync(Guid id, Faculty faculty);
        Task<bool> DeleteFacultyByIdAsync(Guid id);
        Task<Faculty> GetFacultyByIdAsync(Guid id);
        Task<IEnumerable<Faculty>> GetAllFacultiesAsync();
        
    }
}
