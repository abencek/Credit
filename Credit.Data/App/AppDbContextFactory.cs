using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Credit.Data.App
{
    /// <summary>
    /// DbContext factory 
    /// This factory is needed for class library during migration process
    /// </summary>
    
    class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args = null)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:AppDbContext"]);

            return new AppDbContext(optionsBuilder.Options);
        }
    }

}
