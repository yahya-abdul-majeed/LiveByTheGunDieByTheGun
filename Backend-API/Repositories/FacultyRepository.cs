using Backend_API.Models;
using Backend_API.Repositories.RepositoryInterfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Backend_API.Repositories
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly string _connectionString;

        public FacultyRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("SQLConnection");
        }
        public async Task<int> CreateFacultyAsync(string facultyName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO management.faculty(faculty_name)
                            VALUES(@faculty)";
                return await connection.ExecuteAsync(sql,new {faculty =  facultyName})
                    .ConfigureAwait(false);//step out of current context
            }
        }

        public async Task<int> DeleteFacultyByIdAsync(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "DELETE FROM management.faculty WHERE faculty_id = @id";
                return await connection.ExecuteAsync(sql, new { id }).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Faculty>> GetAllFacultiesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM management.faculty";
                var faculties = connection.QueryAsync<Faculty>(sql).ConfigureAwait(false);
                return await faculties;
            }
        }

        public Faculty GetFacultyById(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM management.faculty WHERE faculty_id = @id ";
                var faculty = connection.QuerySingleOrDefault<Faculty>(sql,new {id});
                return faculty;
            }
        }

        public async Task<int> UpdateFacultybyIdAsync(Guid id, string facultyName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "UPDATE management.faculty SET faculty_name = @facultyName WHERE faculty_id = @id";
                return await connection.ExecuteAsync(sql, new { facultyName , id }).ConfigureAwait(false);
            }
        }

    }
}
