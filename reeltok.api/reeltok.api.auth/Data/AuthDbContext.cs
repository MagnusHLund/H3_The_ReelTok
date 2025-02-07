using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.auth.Entites;
using Microsoft.EntityFrameworkCore;

namespace reeltok.api.auth.Data
{
    public class AuthDbContext : DbContext 
    {
       public AuthDbContext(DbContextOptions<AuthDbContext> options) : base (options)
       {
          
       }

       public DbSet<Auth> Auths { get; set; }
       public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
