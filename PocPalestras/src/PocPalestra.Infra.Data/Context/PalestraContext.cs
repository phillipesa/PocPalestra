using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PocPalestra.Domain.Organizadores;
using PocPalestra.Domain.Palestras;
using PocPalestra.Infra.Data.Extensions;
using PocPalestra.Infra.Data.Mappings;
using System.IO;

namespace PocPalestra.Infra.Data.Context
{
    public class PalestraContext : DbContext
    {
        public DbSet<Palestra> Palestras { get; set; }
        public DbSet<Organizador> Organizadores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new PalestraMapping());
            modelBuilder.AddConfiguration(new OrganizadorMapping());
            modelBuilder.AddConfiguration(new EnderecoMapping());
            modelBuilder.AddConfiguration(new CategoriaMapping());

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
