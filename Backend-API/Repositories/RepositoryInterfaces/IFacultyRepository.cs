using Backend_API.Models;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IFacultyRepository
    {
        Task<int> CreateFacultyAsync(string facultyName); 
        Task<int> UpdateFacultybyIdAsync(Guid id, string facultyName);
        Task<int> DeleteFacultyByIdAsync(Guid id);
        Faculty GetFacultyById(Guid id);
        Task<IEnumerable<Faculty>> GetAllFacultiesAsync();
        
    }
}
