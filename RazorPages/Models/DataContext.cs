using Microsoft.EntityFrameworkCore;

namespace RazorPages.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=RazorPages;User=root;Password=root;",
                new MySqlServerVersion(new Version(8, 0, 0)));
        }
    }
}
