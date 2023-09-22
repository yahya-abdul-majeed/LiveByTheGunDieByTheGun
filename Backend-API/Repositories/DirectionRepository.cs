using Backend_API.Models;
using Backend_API.Repositories.RepositoryInterfaces;

namespace Backend_API.Repositories
{
    public class DirectionRepository : IDirectionRepository
    {
        public Task<Direction> CreateDirectionAsync(Direction direction)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDirectionAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Direction>> GetAllDirectionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Direction> GetDirectionByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Direction> UpdateDirectionAsync(Guid id, Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}
