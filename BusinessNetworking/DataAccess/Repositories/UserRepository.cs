using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BusinessNetworking.Entities;
using Dapper;

namespace BusinessNetworking.DataAccess.Repositories
{

    public interface IUserRepository
    {
        Task<int> CreateUser(ClientUser user);
        Task<IEnumerable<ClientUser>> GetAllUsers();
        Task<ClientUser> GetUserById(int userId);
        Task<bool> UpdateUser(ClientUser user);
        Task<bool> DeleteUser(int userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("NetworkingConnection");
        }

        // Resto del código sin cambios...

        public async Task<int> CreateUser(ClientUser user)
        {
            const string sql = @"INSERT INTO dbo.Client (UserName, Name, LastName, Email, PhoneNumber, [Password], TermsAccepted, CreatedDate)
                            VALUES (@UserName, @Name, @LastName, @Email, @PhoneNumber, @Password, @TermsAccepted, @CreatedDate);
                            SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = new SqlConnection(_connectionString))
            {
                user.CreatedDate = DateTime.UtcNow;

                var userId = await connection.ExecuteScalarAsync<int>(sql, user);
                return userId;
            }
        }

        public async Task<IEnumerable<ClientUser>> GetAllUsers()
        {
            const string sql = "SELECT * FROM dbo.Client";

            using (var connection = new SqlConnection(_connectionString))
            {
                var users = await connection.QueryAsync<ClientUser>(sql);
                return users;
            }
        }

        public async Task<ClientUser> GetUserById(int userId)
        {
            const string sql = "SELECT * FROM dbo.Client WHERE UserId = @UserId";

            using (var connection = new SqlConnection(_connectionString))
            {
                var user = await connection.QueryFirstOrDefaultAsync<ClientUser>(sql, new { UserId = userId });
                return user;
            }
        }

        public async Task<bool> UpdateUser(ClientUser user)
        {
            const string sql = @"UPDATE dbo.Client SET UserName = @UserName, Name = @Name, LastName = @LastName,
                                Email = @Email, PhoneNumber = @PhoneNumber, [Password] = @Password,
                                TermsAccepted = @TermsAccepted, CreatedDate = @CreatedDate
                                WHERE UserId = @UserId";

            using (var connection = new SqlConnection(_connectionString))
            {
                var rowsAffected = await connection.ExecuteAsync(sql, user);
                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            const string sql = "DELETE FROM dbo.Client WHERE UserId = @UserId";

            using (var connection = new SqlConnection(_connectionString))
            {
                var rowsAffected = await connection.ExecuteAsync(sql, new { UserId = userId });
                return rowsAffected > 0;
            }
        }
    }
}
