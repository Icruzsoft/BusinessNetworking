using BusinessNetworking.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessNetworking.DataAccess.Repositories.Context
{
    public class NetworkingContext : DbContext
    {
        public NetworkingContext(DbContextOptions<NetworkingContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
