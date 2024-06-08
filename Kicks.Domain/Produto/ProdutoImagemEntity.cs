using Kicks.Models.Produto;

namespace Kicks.Domain.Produto
{
    public class ProdutoImagemEntity
    {
        public Guid ProdutoImagemId { get; set; }
        public Guid ProdutoId { get; set; }
        public string? ImagemUrl { get; set; }
        public string? Descricao { get; set; }

        public static implicit operator ProdutoImagemEntity(ProdutoImagemModel model)
        {
            return new ProdutoImagemEntity() 
            {
                ProdutoImagemId = model.ProdutoImagemId,
                ProdutoId = model.ProdutoId,
                ImagemUrl = model.ImagemUrl,
                Descricao = model.Descricao,
            };
        }

        public static implicit operator ProdutoImagemModel(ProdutoImagemEntity entity)
        {
            return new ProdutoImagemModel()
            {
                ProdutoImagemId = entity.ProdutoImagemId,
                ProdutoId = entity.ProdutoId,
                ImagemUrl = entity.ImagemUrl,
                Descricao = entity.Descricao,
            };
        }
    }
}
