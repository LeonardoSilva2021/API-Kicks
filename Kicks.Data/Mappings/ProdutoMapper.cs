using Kicks.Domain.Produto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kicks.Data.Mappings
{
    public static class ProdutoMapper
    {
        public class Produto : IEntityTypeConfiguration<ProdutoEntity>
        {
            public void Configure(EntityTypeBuilder<ProdutoEntity> b)
            {
                b.HasKey(x => x.ProdutoId);

                b.Property(x => x.Nome);
                b.Property(x => x.Descricao);
                b.Property(x => x.Marca);
                b.Property(x => x.SKU);
                b.Property(x => x.QtdEstoque);
                b.Property(x => x.Preco);
                b.Property(x => x.PrecoPromocao);

                b.ToTable("produto");
            }
        }
    }
}
