using LivrosWebApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivrosWebApi.Data.Contexts.Configurations
{
    public class LivroConfiguration : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasMaxLength(255).HasColumnType("varchar(255)").IsRequired();
            builder.Property(x => x.Ativo)
                .IsRequired()
                .HasColumnType("bit(1)");

            builder.HasOne(x => x.Autor)
                .WithMany(autor => autor.Livros)
                .HasForeignKey(x => x.AutorId);

            builder.HasOne(x => x.Genero)
                .WithMany(genero => genero.Livros)
                .HasForeignKey(x => x.GeneroId);
        }
    }
}
