using Microsoft.EntityFrameworkCore;
using StarWars.Models.Domain;

namespace StarWars.Data
{
    public class MVCDbContext : DbContext
    {
        public MVCDbContext(DbContextOptions options) : base(options) 
        {
        }


        public DbSet<Character> Characters { get; set; }
    }
}
