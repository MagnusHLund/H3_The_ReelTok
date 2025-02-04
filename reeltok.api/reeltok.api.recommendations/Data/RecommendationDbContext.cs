using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.recommendations.Data
{
    using Microsoft.EntityFrameworkCore;
    using reeltok.api.recommendations.Entities;

    public class RecommendationDbContext : DbContext
    {
        public RecommendationDbContext(DbContextOptions<RecommendationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recommendations> Recommendations { get; set; }
    }
}