using Backend_API.Models;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IDirectionRepository
    {
        Task<int> CreateDirectionAsync(Direction direction);
        Task<int> UpdateDirectionAsync(Guid id,Direction direction);
        Task<int> DeleteDirectionAsync(Guid id);
        Direction GetDirectionById(Guid id);
        Task<IEnumerable<Direction>> GetAllDirectionsAsync();
    }
}
