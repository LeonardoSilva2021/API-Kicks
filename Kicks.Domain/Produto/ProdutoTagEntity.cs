using Kicks.Models.Produto;

namespace Kicks.Domain.Produto
{
    public class ProdutoTagEntity
    {
        public Guid ProdutoTagId { get; set; }
        public Guid ProdutoId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }

        public static implicit operator ProdutoTagEntity(ProdutoTagModel model)
        {
            return new ProdutoTagEntity()
            {
                ProdutoTagId = model.ProdutoTagId,
                ProdutoId = model.ProdutoId,
                Nome = model.Nome,
                Descricao = model.Descricao,
            };
        }

        public static implicit operator ProdutoTagModel(ProdutoTagEntity model)
        {
            return new ProdutoTagModel()
            {
                ProdutoTagId = model.ProdutoTagId,
                ProdutoId = model.ProdutoId,
                Nome = model.Nome,
                Descricao = model.Descricao,
            };
        }
    }
}
