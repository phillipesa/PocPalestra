using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PocPalestra.Domain.Core.Events;
using PocPalestra.Infra.Data.Extensions;
using PocPalestra.Infra.Data.Mappings;
using System.IO;

namespace PocPalestra.Infra.Data.Context
{
    public class EventStoreSQLContext : DbContext            
    {
        public DbSet<StoredEvent> StoredEvent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new StoredEventMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}
