using BusinessNetworking.DataAccess.Repositories;
using BusinessNetworking.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BusinessNetworking.Services
{
    public interface IUserService
    {
        Task<int> RegisterUser(User user);
        Task<string> AuthenticateUser(string email, string password);
    }

    public class UserService : IUserService
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("NetworkingConnection");
            _configuration = configuration;
        }

        public async Task<int> RegisterUser(User user)
        {
            const string sql = @"INSERT INTO dbo.Client (UserName, Name, LastName, Email, PhoneNumber, Password, TermsAccepted, CreatedDate)
                            VALUES (@UserName, @Name, @LastName, @Email, @PhoneNumber, @Password, @TermsAccepted, @CreatedDate);
                            SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = new SqlConnection(_connectionString))
            {
                user.CreatedDate = DateTime.UtcNow;
                var userId = await connection.ExecuteScalarAsync<int>(sql, user);
                return userId;
            }
        }

        public async Task<string> AuthenticateUser(string email, string password)
        {
            var user = await GetUserByEmail(email);
            if (user != null && password == user.Password)
            {
                return GenerateJwtToken(user);
            }
            return null;
        }

        private async Task<User> GetUserByEmail(string email)
        {
            const string sql = "SELECT * FROM dbo.Client WHERE Email = @Email";
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
