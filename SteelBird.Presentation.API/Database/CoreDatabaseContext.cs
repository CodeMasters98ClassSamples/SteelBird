using Microsoft.EntityFrameworkCore;
using SteelBird.Presentation.API.Entities;

namespace SteelBird.Presentation.API.Database
{
    public class CoreDatabaseContext : DbContext
    {
        public CoreDatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
