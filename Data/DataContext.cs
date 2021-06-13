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
    }
}