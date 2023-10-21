using Backend_API.Models;
using Backend_API.Models.DTO;

namespace Backend_API.Repositories.RepositoryInterfaces
{
    public interface IDirectionRepository
    {
        Task<int> CreateDirectionAsync(Direction direction);
        Task<int> UpdateDirectionAsync(Guid id,Direction direction);
        Task<int> DeleteDirectionAsync(Guid id);
        Direction GetDirectionById(Guid id);
        DirectionDTO GetDirectionWithFacutly(Guid id);
        Task<IEnumerable<Direction>> GetAllDirectionsAsync();
    }
}
