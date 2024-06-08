namespace Kicks.Models.Produto
{
    public class ProdutoImagemModel
    {
        public Guid ProdutoImagemId { get; set; }
        public Guid ProdutoId { get; set; }
        public string? ImagemUrl { get; set; }
        public string? Descricao { get; set; }
    }
}
