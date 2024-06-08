using Kicks.Models.Produto;
using Kicks.Services.Services.Produto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kicks.API.Controllers.V1.Produto
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProdutoController : ControllerBase
    {
        #region Dependências
        private readonly IProdutoService _produtoService;
        #endregion

        #region Contrutor
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }
        #endregion

        #region Criar Produto
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProdutoModel>> CriarProduto(ProdutoModel model)
        {
            var res = await _produtoService.CriarProduto(model);
            return Ok(res);
        }
        #endregion

        #region Editar Produto
        [HttpPut("{produtoId:guid}")]
        [Authorize]
        public async Task<ActionResult<ProdutoModel>> EditarProduto(Guid produtoId, ProdutoModel model)
        {
            await _produtoService.EditarProduto(produtoId, model);
            return NoContent();
        }
        #endregion

        #region Obter Todos os Produtos
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ICollection<ProdutoModel>>> ObterProdutos()
        {
            var res = await _produtoService.ObterProdutos();
            return Ok(res);
        } 
        #endregion

        #region Obter Produto Por Id
        [HttpGet("{produtoId:guid}")]
        [Authorize]
        public async Task<ActionResult<ProdutoModel>> ObterProdutoById(Guid produtoId)
        {
            var res = await _produtoService.ObterProdutoById(produtoId);
            return Ok(res);
        }
        #endregion

        #region Deletar Produto
        [HttpDelete("{produtoId:Guid}")]
        [Authorize]
        public async Task<ActionResult<ProdutoModel>> DeletarProduto(Guid produtoId)
        {
           await _produtoService.DeletarProduto(produtoId);
            return NoContent();
        } 
        #endregion
    }
}
