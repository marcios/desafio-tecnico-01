using LivrosWebApi.Core.Entities;
using LivrosWebApi.Data.Contexts.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LivrosWebApi.Data.Contexts
{
    public class LivrosDbContext : DbContext
    {
        public DbSet<Livro> Generos { get; set; }
        public DbSet<Autor> Autores { get; set; }

        public DbSet<Livro> Livros { get; set; }

        public LivrosDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GeneroConfiguration).Assembly);
        }
    }
}
