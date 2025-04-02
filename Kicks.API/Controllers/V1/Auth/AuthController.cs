using Kicks.Models.Auth;
using Kicks.Services.Services.Auth.Classes;
using Microsoft.AspNetCore.Mvc;

namespace Kicks.API.Controllers.V1.Auth
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Dependências
        private readonly IAuthService _authService;
        #endregion

        #region Construtor
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        #endregion

        #region Autenticar
        [HttpPost]
        public async Task<ActionResult<TokenModel>> Autenticar([FromBody] AutenticarModel model)
        {
            var res = await _authService.Autenticar(model);
            return Ok(res);
        } 
        #endregion
    }
}
