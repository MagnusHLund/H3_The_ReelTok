using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommentsService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentsService.Data
{
    public class CommentDbContext : DbContext
    {

        public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }

    }
}
