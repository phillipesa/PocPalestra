using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PocPalestra.Infra.CrossCutting.Identity.Data;
using System.IO;

namespace PocPalestra.Infra.CrossCutting.Identity
{
    public class DesignFactoryContext : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        // Factroy para utilização do add migration e update database
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new ApplicationDbContext(optionsBuilder.Options);

        }
    }
}
