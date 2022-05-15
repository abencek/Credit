using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Credit.Data.Identity
{
    /// <summary>
    /// DbContext factory 
    /// Factory is necessary for class library during migration process
    /// </summary>
    
    class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args = null)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:IdentityDbContext"]);

            return new IdentityDbContext(optionsBuilder.Options);
        }
    }

}
