using Backend_API.Models;
using Backend_API.Repositories.RepositoryInterfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Backend_API.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly string _connectionString;

        public GroupRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("SQLConnection");
        }
        public Guid CreateGroup(Group group)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var(_direction_id, _group_name) = group;
                var sql = @"
                        INSERT INTO management.student_group (direction_id, group_name)
                        OUTPUT inserted.group_id
                        VALUES (@_direction_id,@_group_name)
                        ";
                return connection.QuerySingle<Guid>(sql, new {_direction_id,_group_name});
            }
        }

        public async Task<int> DeleteGroupAsync(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                        DELETE FROM management.student_group 
                        WHERE group_id = @id
                        ";
                return await connection.ExecuteAsync(sql, new {id});
            }
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                        SELECT * FROM management.student_group
                        ";
                return await connection.QueryAsync<Group>(sql);
            }
        }

        public Group GetGroupById(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                        SELECT * FROM management.student_group
                        WHERE group_id = @id
                        ";
                return connection.QuerySingleOrDefault<Group>(sql,new {id});
            }
        }

        public Group GetGroupByName(string Name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                        SELECT * FROM management.student_group
                        WHERE group_name = @Name  
                        ";
                return connection.QuerySingleOrDefault<Group>(sql,new {Name});
            }
        }

        public async Task<int> UpdateGroupAsync(Guid id, Group group)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var (_direction_id, _group_name) = group;
                var sql = @"
                        UPDATE management.student_group SET 
                        group_name = @_group_name,
                        direction_id = @_direction_id
                        WHERE group_id = @id
                        ";
                return await connection.ExecuteAsync(sql,new {_direction_id,_group_name,id});
            }
        }
    }
}
