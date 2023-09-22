using Backend_API.Models;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IDirectionRepository
    {
        Task<Direction> CreateDirectionAsync(Direction direction);
        Task<Direction> UpdateDirectionAsync(Guid id,Direction direction);
        Task<bool> DeleteDirectionAsync(Guid id);
        Task<Direction> GetDirectionByIdAsync(Guid id);
        Task<IEnumerable<Direction>> GetAllDirectionsAsync();
    }
}
