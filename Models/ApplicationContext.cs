using Microsoft.EntityFrameworkCore;
namespace BlogMVC.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Post> Posts { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   
        }
    }
}
