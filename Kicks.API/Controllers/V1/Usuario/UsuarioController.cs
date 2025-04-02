using Kicks.Models.Usuario;
using Kicks.Services.Services.Usuario.Classe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kicks.API.Controllers.V1.Usuario
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuarioController : ControllerBase
    {
        #region Dependências
        private readonly IUsuarioService _usuarioService;
        #endregion

        #region Construtor
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        } 
        #endregion

        #region Criar Usuario
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> CriarUsuario([FromBody] UsuarioModel model)
        {
            var res = await _usuarioService.CriarUsuario(model);
            return Ok(res);
        }
        #endregion

        #region Obter Usuario Por Id
        [HttpGet("{usuarioId:guid}")]
        [Authorize]
        public async Task<ActionResult<UsuarioModel>> ObterById(Guid usuarioId)
        {
            var res = await _usuarioService.ObterById(usuarioId);
            return Ok(res);
        }
        #endregion

        #region Ediitar Usuario
        [HttpPut("{usuarioId:guid}")]
        [Authorize]
        public async Task<ActionResult<UsuarioModel>> EditarUsuario(Guid usuarioId, [FromBody] UsuarioModel model)
        {
            var res = await _usuarioService.EditarUsuario(usuarioId, model);
            return Ok(res);
        }
        #endregion

        #region Deletar Usuario
        [HttpDelete("{usuarioId:guid}")]
        [Authorize]
        public async Task<NoContentResult> DeletarUsuario(Guid usuarioId)
        {
            await _usuarioService.DeletarUsuario(usuarioId);
            return NoContent();
        }
        #endregion
    }
}
