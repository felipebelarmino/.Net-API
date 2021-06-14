using Dot_Net_Core_API_with_JWT.Models;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_Core_API_with_JWT.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Character> Characters { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Weapon> Weapons { get; set; }

    public DbSet<Skill> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Skill>().HasData(
          new Skill { Id = 1, Name = "Fireball", Damage = 30 },
          new Skill { Id = 2, Name = "Frenzy", Damage = 20 },
          new Skill { Id = 3, Name = "Kick", Damage = 10 },
          new Skill { Id = 4, Name = "Punch", Damage = 5 }
      );

      modelBuilder.Entity<User>()
        .Property(user => user.Role).HasDefaultValue("Player"); //All new users have default role Player
    }
  }
}