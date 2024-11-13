using Microsoft.EntityFrameworkCore;
using WebApplication73.Entities;

namespace WebApplication73
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
    }
}
