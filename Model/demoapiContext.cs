using Microsoft.EntityFrameworkCore;
using demoapi.Models;

namespace demoapi.Models
{
    public class demoapiContext : DbContext
    {
        public demoapiContext(DbContextOptions<demoapiContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}