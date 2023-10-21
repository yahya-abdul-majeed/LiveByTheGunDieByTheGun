using Backend_API.Models;
using Backend_API.Models.DTO;
using Backend_API.Repositories.RepositoryInterfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Backend_API.Repositories
{
    public class DirectionRepository : IDirectionRepository
    {
        private readonly string _connectionString;

        public DirectionRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("SQLConnection");
        }
        public async Task<int> CreateDirectionAsync(Direction direction)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var (name, facultyid) = direction;
                var sql = @"INSERT INTO management.direction (faculty_id,direction_name)
                            VALUES(@facultyid,@name)";
                return await connection.ExecuteAsync(sql, new { name, facultyid }).ConfigureAwait(false);
            }
        }

        public async Task<int> DeleteDirectionAsync(Guid id)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE FROM management.direction WHERE direction_id = @id";
                return await connection.ExecuteAsync(sql, new { id }).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Direction>> GetAllDirectionsAsync()
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM management.direction";
                return await connection.QueryAsync<Direction>(sql).ConfigureAwait(false);
            }
        }

        public Direction GetDirectionById(Guid id)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM management.direction WHERE direction_id = @id";
                return connection.QuerySingleOrDefault<Direction>(sql,new {id});
            }
        }

        public DirectionDTO GetDirectionWithFacutly(Guid id)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    SELECT a.direction_id,a.direction_name, b.faculty_name,b.faculty_id FROM management.direction a
                    LEFT OUTER JOIN management.faculty b ON a.faculty_id = b.faculty_id
                    WHERE a.direction_id = @id
                        ";
                return connection.QuerySingleOrDefault<DirectionDTO>(sql,new {id});
            }
        }

        public async Task<int> UpdateDirectionAsync(Guid id, Direction direction)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var (name,facultyid) = direction; // could cause errors due to naming of faculty id 
                var sql = @"UPDATE management.direction SET
                            direction_name = @name,
                            faculty_id = @facultyid
                            WHERE direction_id = @id";
                return await connection.ExecuteAsync(sql,new {name, facultyid, id}).ConfigureAwait(false);
            }
        }
    }
}
