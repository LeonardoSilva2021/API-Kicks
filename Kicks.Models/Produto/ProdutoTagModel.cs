namespace Kicks.Models.Produto
{
    public class ProdutoTagModel
    {
        public Guid ProdutoTagId { get; set; }
        public Guid ProdutoId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
    }
}
