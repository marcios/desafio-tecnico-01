using LivrosWebApi.Core.Entities;
using LivrosWebApi.Data.Contexts.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LivrosWebApi.Data.Contexts
{
    public class LivrosDbContext : DbContext
    {
        public DbSet<Genero> Generos { get; set; }

        public LivrosDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeia as configurações de classe para tabelas.
            // Mapeia as configurações de classe para tabelas.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GeneroConfiguration).Assembly);
        }
    }
}
