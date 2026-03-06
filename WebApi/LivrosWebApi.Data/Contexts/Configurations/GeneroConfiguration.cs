using LivrosWebApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivrosWebApi.Data.Contexts.Configurations
{
    public class GeneroConfiguration : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Genero");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasMaxLength(100).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Ativo)
                .IsRequired()
                .HasColumnType("bit(1)");
        }
    }
}
