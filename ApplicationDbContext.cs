using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using QuizApp.Models;

namespace QuizApp
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }

    public class DesignTimeBMDbContext : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // pass your design time connection string here
            optionsBuilder.UseSqlServer("Server=mssqlserver,1433;Database=AppDb;User Id=sa;Password=Password1*;TrustServerCertificate=true");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
