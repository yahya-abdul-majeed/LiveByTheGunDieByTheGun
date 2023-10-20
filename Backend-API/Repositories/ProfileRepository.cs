using Backend_API.Models.DTO;
using Backend_API.Refit;
using Backend_API.Repositories.RepositoryInterfaces;
using Backend_API.Services.ServiceInterfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Backend_API.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IAvatarService _avatarservice;
        private readonly string _connectionString;

        public ProfileRepository(IAvatarService  avatarService, IConfiguration config)
        {
            _avatarservice = avatarService;
            _connectionString = config.GetConnectionString("SQLConnection");
        }
        
        public async Task<int> ChangeUserAvatar(Guid userid)
        {
            var avatar = await _avatarservice.GetCustomAvatar();
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                            UPDATE management.applicationuser SET avatar = @avatar
                            WHERE user_id = @userid
                         ";
                return await connection.ExecuteAsync(sql, new {avatar,userid});
            }

        }

        public async Task<int> EditUserProfile(Guid userid, EditProfileDTO dto)
        {
            var(_username, _phone, _birthdate) = dto;
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                        UPDATE management.applicationuser SET 
                        username = @_username,
                        phone = @_phone,
                        birthdate = @_birthdate
                        WHERE user_id = @userid
                        ";
                return await connection.ExecuteAsync(sql, new { userid, _username, _phone, _birthdate });
            }
        }
    }
}
