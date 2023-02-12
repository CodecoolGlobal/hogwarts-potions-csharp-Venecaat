using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Models
{
    public class HogwartsContext : DbContext
    {
        public HogwartsContext(DbContextOptions<HogwartsContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; } = default!;
        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<Recipe> Recipes { get; set; } = default!;
        public DbSet<Ingredient> Ingredients { get; set; } = default!;
        public DbSet<Potion> Potions { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Student>().Navigation(student => student.Room).AutoInclude();
            _ = modelBuilder.Entity<Room>().Navigation(room => room.Residents).AutoInclude();
            _ = modelBuilder.Entity<Recipe>().Navigation(recipe => recipe.Student).AutoInclude();
            _ = modelBuilder.Entity<Recipe>().Navigation(recipe => recipe.Ingredients).AutoInclude();
            _ = modelBuilder.Entity<Potion>().Navigation(potion => potion.Student).AutoInclude();
            _ = modelBuilder.Entity<Potion>().Navigation(potion => potion.Ingredients).AutoInclude();
        }
    }
}
