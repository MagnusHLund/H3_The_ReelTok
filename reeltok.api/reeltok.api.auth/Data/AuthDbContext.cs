using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reeltok.api.auth.Entites;

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
