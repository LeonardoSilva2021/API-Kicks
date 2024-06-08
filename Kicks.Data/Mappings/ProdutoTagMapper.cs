using Kicks.Domain.Produto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kicks.Data.Mappings
{
    public static class ProdutoTagMapper
    {
        public class ProdutoTag : IEntityTypeConfiguration<ProdutoTagEntity>
        {
            public void Configure(EntityTypeBuilder<ProdutoTagEntity> b)
            {
                b.HasKey(x => x.ProdutoTagId);

                b.Property(x => x.ProdutoId);
                b.Property(x => x.Nome);
                b.Property(x => x.Descricao);

                b.ToTable("produtoTag");
            }
        }
    }
}
