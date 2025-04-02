using Kicks.Domain.Categoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kicks.Data.Mappings
{
    public static class CategoriaMapper
    {
        public class Categoria : IEntityTypeConfiguration<CategoriaEntity>
        {
            public void Configure(EntityTypeBuilder<CategoriaEntity> b)
            {
                b.HasKey(x => x.CategoriaId);

                b.Property(x => x.UsuarioId);
                b.Property(x => x.Nome);
                b.Property(x => x.Tipo);
                b.Property(x => x.ImagemUrl);

                b.ToTable("categoria");
            }
        }
    }
}
