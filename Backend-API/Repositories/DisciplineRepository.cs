using Backend_API.Models;
using Backend_API.Repositories.RepositoryInterfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Backend_API.Repositories
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private readonly string _connectionString;

        public DisciplineRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("SQLConnection");
        }
        public async Task<int> CreateDisciplineAsync(Discipline discipline)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO management.discipline(discipline_name,description,literature,year,grade_id,is_online,building,room)
                            VALUES(@name,@_description,@_literature,@_year,@_grade_id,@_is_online,@_building,@_room)";
                var (name, _description,_literature,_year,_grade_id,_is_online,_building,_room) = discipline;
                return await connection.ExecuteAsync(sql, new {name,_description,_literature,_year,_grade_id,_is_online,_building,_room }).ConfigureAwait(false);    
            }
        }

        public async Task<int> DeleteDisciplineAsync(Guid id)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE FROM management.discipline WHERE discipline_id = @id";
                return await connection.ExecuteAsync(sql, new {id}).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Discipline>> GetAllDisciplinesAsync()
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM management.discipline";
                return await connection.QueryAsync<Discipline>(sql).ConfigureAwait(false);
            }
        }

        public  Discipline GetDisciplineById(Guid id)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM management.discipline WHERE discipline_id = @id";
                return connection.QuerySingleOrDefault<Discipline>(sql, new {id});
            }
        }

        public async Task<int> UpdateDisciplineAsync(Guid id, Discipline discipline)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE management.discipline SET
                            discipline_name = @name,
                            description = @_description,
                            literature = @_literature,
                            year = @_year,
                            grade_id = @_grade_id,
                            is_online = @_is_online,
                            building = @_building,
                            room = @_room
                            WHERE discipline_id = @id
                            ";
                var (name, _description,_literature,_year,_grade_id,_is_online,_building,_room) = discipline;

                return await connection.ExecuteAsync(sql, new { name, _description, _literature, _year, _grade_id, _is_online, _building, _room }).ConfigureAwait(false);
            }
        }
    }
}
