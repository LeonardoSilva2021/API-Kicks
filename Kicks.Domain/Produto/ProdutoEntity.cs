using Kicks.Models.Produto;

namespace Kicks.Domain.Produto
{
    public class ProdutoEntity
    {
        public Guid ProdutoId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Marca { get; set; }
        public string? SKU { get; set; }
        public int QtdEstoque { get; set; }
        public double Preco { get; set; }
        public double PrecoPromocao { get; set; }
        public ICollection<ProdutoImagemEntity>? Imagens { get; set; }
        public ICollection<ProdutoTagEntity>? Tags { get; set; }

        public static implicit operator ProdutoEntity(ProdutoModel model)
        {
            return new ProdutoEntity()
            {
                ProdutoId = model.ProdutoId,
                Nome = model.Nome,
                Descricao = model.Descricao,
                Marca = model.Marca,
                SKU = model.SKU,
                QtdEstoque = model.QtdEstoque,
                Preco = model.Preco,
                PrecoPromocao = model.PrecoPromocao,
                Imagens = model.Imagens?.Select(x => (ProdutoImagemEntity)x).ToList(),
                Tags = model.Tags?.Select(x => (ProdutoTagEntity)x).ToList(),
            };
        }

        public static implicit operator ProdutoModel(ProdutoEntity entity)
        {
            return new ProdutoModel()
            {
                ProdutoId = entity.ProdutoId,
                Nome = entity.Nome,
                Descricao = entity.Descricao,
                Marca = entity.Marca,
                SKU = entity.SKU,
                QtdEstoque = entity.QtdEstoque,
                Preco = entity.Preco,
                PrecoPromocao = entity.PrecoPromocao,
                Imagens = entity.Imagens?.Select(x => (ProdutoImagemModel)x).ToList(),
                Tags = entity.Tags?.Select(x => (ProdutoTagModel)x).ToList(),
            };
        }
    }
}
