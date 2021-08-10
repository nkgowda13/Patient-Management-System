using AuthorizationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationService.Provider
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> users { get; set; }
    }
}
