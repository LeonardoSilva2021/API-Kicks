using Kicks.Domain.Produto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kicks.Data.Mappings
{
    public static class ProdutoImagemMapper
    {
        public class ProdutoImagem : IEntityTypeConfiguration<ProdutoImagemEntity>
        {
            public void Configure(EntityTypeBuilder<ProdutoImagemEntity> b)
            {
                b.HasKey(x => x.ProdutoImagemId);

                b.Property(x => x.ProdutoId);
                b.Property(x => x.ImagemUrl);
                b.Property(x => x.Descricao);

                b.ToTable("produtoImagem");
            }
        }
    }
}
