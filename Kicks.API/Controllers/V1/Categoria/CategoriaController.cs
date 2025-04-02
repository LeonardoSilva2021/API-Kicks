using Kicks.Models.Categoria;
using Kicks.Services.Services.Categoria.Classe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kicks.API.Controllers.V1.Categoria
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriaController : ControllerBase
    {
        #region Dependências
        private readonly ICategoriaService _categoriaServise;
        #endregion

        #region Construtor
        public CategoriaController(ICategoriaService categoriaServise)
        {
            _categoriaServise = categoriaServise;
        }
        #endregion

        #region Criar Categoria
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CategoriaModel>> CriarCategoria(CategoriaModel model)
        {
            var res = await _categoriaServise.CriarCategoria(model);
            return Ok(res);
        }
        #endregion

        #region Editar Categoria
        [HttpPut("{categoriaId:guid}")]
        [Authorize]
        public async Task<ActionResult<CategoriaModel>> EditarCategoria(Guid categoriaId, CategoriaModel model)
        {
            await _categoriaServise.EditarCategotia(categoriaId, model);
            return NoContent();
        }
        #endregion

        #region Obter Todas as Categorias
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ICollection<CategoriaModel>>> ObterCategorias()
        {
            var res = await _categoriaServise.ObterCategorias();
            return Ok(res);
        }
        #endregion

        #region Obter Categoria Por Id
        [HttpGet("{categoriaId:guid}")]
        [Authorize]
        public async Task<ActionResult<CategoriaModel>> ObterCategoriaById(Guid categoriaId)
        {
            var res = await _categoriaServise.ObterCategoriaById(categoriaId);
            return Ok(res);
        }
        #endregion

        #region Deletar Categoria
        [HttpDelete("{categoriaId:guid}")]
        [Authorize]
        public async Task<ActionResult<CategoriaModel>> DeletarCategoria(Guid categoriaId)
        {
            await _categoriaServise.DeletarCategoria(categoriaId);
            return NoContent();
        } 
        #endregion
    }
}
