using Microsoft.EntityFrameworkCore;

namespace MasterDegree.Models
{
    public class MasterDegreeDbContext: DbContext
    {
        public MasterDegreeDbContext(DbContextOptions<MasterDegreeDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFavorites> UserFavorites { get; set; }
    }
}
