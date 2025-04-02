using Kicks.Models.Produto;

namespace Kicks.Services.Services.Produto.Classe
{
    public interface IProdutoService
    {
        public Task<ProdutoModel> CriarProduto(ProdutoModel model);
        public Task<ProdutoModel> EditarProduto(Guid produtoId, ProdutoModel model);
        public Task<ICollection<ProdutoModel>> ObterProdutos();
        public Task<ProdutoModel> ObterProdutoById(Guid produtoId);
        public Task<ProdutoModel> DeletarProduto(Guid produtoId);
    }
}
