using Microsoft.EntityFrameworkCore;
using reeltok.api.auth.Entities;

namespace reeltok.api.auth.Data
{
    public class AuthDbContext : DbContext
    {
       public AuthDbContext(DbContextOptions<AuthDbContext> options) : base (options)
       {

       }

       public DbSet<UserAuthentication> Auths { get; set; }
       public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
