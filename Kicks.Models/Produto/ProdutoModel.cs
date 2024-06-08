namespace Kicks.Models.Produto
{
    public class ProdutoModel
    {
        public Guid ProdutoId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Marca { get; set; }
        public string? SKU { get; set; }
        public int QtdEstoque { get; set; }
        public double Preco { get; set; }
        public double PrecoPromocao { get; set; }
        public ICollection<ProdutoImagemModel>? Imagens { get; set; }
        public ICollection<ProdutoTagModel>? Tags { get; set; }
    }
}
