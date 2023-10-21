using Backend_API.Models;
using Backend_API.Models.DTO;
using Backend_API.Repositories.RepositoryInterfaces;
using Backend_API.Services.ServiceInterfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

namespace Backend_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly IEmailService _emailService;

        public UserRepository(IConfiguration config, IEmailService emailService)
        {
            _connectionString = config.GetConnectionString("SQLConnection");
            _emailService = emailService;
        }

        public Guid CreateTeacherUser(JailedUser jailee, string _password)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var (name, _birthdate, _email, _phone, _avatar, _is_student, _faculty_id, _direction_id, _group_id, _grade_id) = jailee;
                var sql = @"Declare @temptable TABLE (
                        user_id UNIQUEIDENTIFIER
                        )
                        INSERT INTO management.applicationuser 
                        (
                            username,
                            birthdate,
                            email,
                            phone,
                            avatar,
                            password
                        ) 
                        OUTPUT inserted.user_id INTO @temptable(user_id)
                        OUTPUT inserted.user_id
                        VALUES
                        (
                            @name,
                            @_birthdate,
                            @_email,
                            @_phone,
                            @_avatar,
                            @_password
                        )

                        INSERT INTO management.teacher (user_id)
                        SELECT user_id FROM @temptable";
                var result = connection.QuerySingle<Guid>(sql, new
                {
                    name,
                    _birthdate,
                    _email,
                    _phone,
                    _avatar,
                    _is_student,
                    _faculty_id,
                    _direction_id,
                    _group_id,
                    _grade_id,
                    _password
                });
                return result;

            }
        }

        public Guid CreateStudentUser(JailedUser jailee, string _password)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var (name, _birthdate, _email, _phone, _avatar, _is_student, _faculty_id, _direction_id, _group_id, _grade_id) = jailee;
                var sql = @"Declare @temptable TABLE (
                           id UNIQUEIDENTIFIER
                        )
                        DECLARE @user_id UNIQUEIDENTIFIER;

                        INSERT INTO management.applicationuser 
                        (
                            username,
                            birthdate,
                            email,
                            phone,
                            avatar,
                            password
                        ) 
                        OUTPUT inserted.user_id INTO @temptable(id)
                        OUTPUT inserted.user_id
                        VALUES
                        (
                            @name,
                            @_birthdate,
                            @_email,
                            @_phone,
                            @_avatar,
                            @_password
                        )
                        SELECT @user_id = id FROM @temptable;

                        INSERT INTO management.student (user_id,faculty_id,direction_id,group_id,grade_id)
                        VALUES(@user_id, @_faculty_id, @_direction_id, @_group_id, @_grade_id)
                        ";
                var result = connection.QuerySingle<Guid>(sql, new
                {
                    name,
                    _birthdate,
                    _email,
                    _phone,
                    _avatar,
                    _is_student,
                    _faculty_id,
                    _direction_id,
                    _group_id,
                    _grade_id,
                    _password
                });
                return result;

            }
        }

        public JailedUser GetJailedUser(Guid id)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM management.jaileduser
                            WHERE user_id = @id";
                return connection.QueryFirstOrDefault<JailedUser>(sql, new { id });
            }
        }

        public ApplicationStudentDTO GetStudent(Guid id)
        {
            using(var connection = new SqlConnection(_connectionString)) {
                var sql = @"
                SELECT s.user_id, a.username, a.birthdate,a.email,a.phone,
                a.avatar,s.faculty_id,s.group_id,s.grade_id,s.direction_id,
                f.faculty_name,d.direction_name,lg.grade_name,sg.group_name
                                
                FROM management.applicationuser a
                RIGHT OUTER JOIN management.student s ON a.user_id = s.user_id
                LEFT OUTER JOIN management.faculty f ON s.faculty_id = f.faculty_id
                LEFT OUTER JOIN management.direction d ON d.direction_id = s.direction_id
                LEFT OUTER JOIN management.l_grade lg ON lg.grade_id = s.grade_id
                LEFT OUTER JOIN management.student_group sg ON sg.group_id = s.group_id
                WHERE s.user_id = @id
                ";
                return connection.QuerySingleOrDefault<ApplicationStudentDTO >(sql, new { id });
            }
        }

        public async Task<IEnumerable<ApplicationStudentDTO>> GetStudentsAsync()
        {
            using(var connection = new SqlConnection(_connectionString)) {
                var sql = @"
                SELECT s.user_id, a.username, a.birthdate,a.email,a.phone,
                a.avatar,s.faculty_id,s.group_id,s.grade_id,s.direction_id,
                f.faculty_name,d.direction_name,lg.grade_name,sg.group_name
                                
                FROM management.applicationuser a
                RIGHT OUTER JOIN management.student s ON a.user_id = s.user_id
                LEFT OUTER JOIN management.faculty f ON s.faculty_id = f.faculty_id
                LEFT OUTER JOIN management.direction d ON d.direction_id = s.direction_id
                LEFT OUTER JOIN management.l_grade lg ON lg.grade_id = s.grade_id
                LEFT OUTER JOIN management.student_group sg ON sg.group_id = s.group_id
                ";
                return await connection.QueryAsync<ApplicationStudentDTO>(sql);
            }
        }

        public ApplicationTeacherDTO GetTeacher(Guid id)
        {
            using(var connection = new SqlConnection(_connectionString)) {
                var sql = @"
                SELECT t.user_id, a.username, a.birthdate,a.email,a.phone,
                a.avatar

                FROM management.applicationuser a
                RIGHT OUTER JOIN management.teacher t ON 
                a.user_id = t.user_id
                WHERE t.user_id = @id
                ";
                return connection.QuerySingleOrDefault<ApplicationTeacherDTO>(sql, new { id });
            }
        }

        public async Task<IEnumerable<ApplicationTeacherDTO>> GetTeachersAsync()
        {
            using(var connection = new SqlConnection(_connectionString)) {
                var sql = @"
                SELECT t.user_id, a.username, a.birthdate,a.email,a.phone,
                a.avatar

                FROM management.applicationuser a
                RIGHT OUTER JOIN management.teacher t ON 
                a.user_id = t.user_id
                ";
                return await connection.QueryAsync<ApplicationTeacherDTO>(sql);
            }
        }

        public async Task<int> AssignUserRoleTo(Guid userid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    DECLARE @roleid UNIQUEIDENTIFIER;

                    SELECT @roleid = role_id FROM management.approle
                    WHERE role_name = 'User'

                    INSERT INTO management.a_user_approle (role_id,user_id)
                    VALUES (@roleid,@userid)
                ";
                return await connection.ExecuteAsync(sql, new { userid });
            }
        }

        public async Task<int> AssignAdminRoleTo(Guid userid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    DECLARE @roleid UNIQUEIDENTIFIER;

                    SELECT @roleid = role_id FROM management.approle
                    WHERE role_name = 'Admin'

                    INSERT INTO management.a_user_approle (role_id,user_id)
                    VALUES (@roleid,@userid)
                ";
                return await connection.ExecuteAsync(sql, new { userid });
            }
        }

        public async Task<int> SetUserPassword(string email, string password, byte[] token)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                         DECLARE @currenttime DATETIME;
                         SET @currenttime = GETDATE()
                         SELECT CASE WHEN EXISTS(
                            SELECT * FROM management.resetticket
                            WHERE email = @email AND tokenhash = @token
                            AND used = 0 AND (@currenttime < expires)
                         )
                         THEN CAST(1 AS BIT)
                         ELSE CAST(0 AS BIT) END;
                        ";
                var result = connection.QuerySingleOrDefault<bool>(sql, new { email, token });
                if (result)
                {
                    sql = @"
                            UPDATE management.applicationuser SET password = @password
                            WHERE email = @email

                            UPDATE management.resetticket SET used = 1
                            WHERE email = @email AND tokenhash = @token
                            AND used = 0

                            ";
                    return await connection.ExecuteAsync(sql, new { email, password, token });
                }
                return 0;
            }
        }

        public async Task<byte[]> SendChangePasswordEmail(string email,string link)
        {
            byte[] token = new byte[16];
            RNGCryptoServiceProvider rngCsp = new();
            rngCsp.GetBytes(token);

            var expires = DateTime.Now.AddMinutes(15);
            var used = false;
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                        INSERT INTO management.resetticket (email, tokenhash, expires, used)
                        VALUES(@email,@token,@expires,@used)
                        ";
                await connection.ExecuteAsync(sql, new {email,expires,used,token}); 
            }

            var redirectTo = link + "?token=" + token;
            _emailService.SendResetPasswordEmail(email, redirectTo);

            return token;
        }
    }
}
