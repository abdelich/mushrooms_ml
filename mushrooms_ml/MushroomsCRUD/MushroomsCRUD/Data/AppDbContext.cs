using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MushroomsCRUD.Data
{
    internal class AppDbContext: DbContext
    {
        public DbSet<Mushroom> Mushrooms { get; set; }
        public DbSet<Gill> Gills { get; set; }
        public DbSet<Stalk> Stalks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source=C:\\Mine\\Study\\NURE\\4_1\\NETCore\\pz23\\mushrooms_ml\\MushroomsCRUD\\MushroomsCRUD\\Data\\mushrooms.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mushroom>()
                .HasOne(m => m.Gill)
                .WithOne()
                .HasForeignKey<Gill>(g => g.Id);

            modelBuilder.Entity<Mushroom>()
                .HasOne(m => m.Stalk)
                .WithOne()
                .HasForeignKey<Stalk>(s => s.Id);
        }
    }
}
