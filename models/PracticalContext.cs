using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace PracticeApi.Models
{
    public class PracticalContext : DbContext
    {
        public PracticalContext(DbContextOptions<PracticalContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
        public DbSet<Hero> Heroes {get; set;} = null!;
    }
}