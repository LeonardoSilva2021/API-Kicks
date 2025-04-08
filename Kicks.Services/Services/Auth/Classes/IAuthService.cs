using Kicks.Models.Auth;
using Kicks.Models.Usuario;

namespace Kicks.Services.Services.Auth.Classes
{
    public interface IAuthService
    {
        public Task<string> GenereteToken(UsuarioModel model);
        public Task<TokenModel> Autenticar(AutenticarModel model);
    }
}
