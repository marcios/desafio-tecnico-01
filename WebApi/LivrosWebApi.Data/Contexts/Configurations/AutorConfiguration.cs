using LivrosWebApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivrosWebApi.Data.Contexts.Configurations
{
    public class AutorConfiguration : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("Autor");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasMaxLength(100).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Ativo)
                .IsRequired()
                .HasColumnType("bit(1)");
        }
    }
}
