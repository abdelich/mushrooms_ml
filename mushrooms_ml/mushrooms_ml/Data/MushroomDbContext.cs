using Microsoft.EntityFrameworkCore;
using mushrooms_ml.Models;

namespace mushrooms_ml.Data
{
    public class MushroomDbContext: DbContext
    {
        public DbSet<Mushroom> Mushrooms { get; set; }

        public MushroomDbContext(DbContextOptions<MushroomDbContext> options) : base(options) { }
    }
}
