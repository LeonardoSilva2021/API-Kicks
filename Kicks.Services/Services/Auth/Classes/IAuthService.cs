using Kicks.Models.Auth;

namespace Kicks.Services.Services.Auth.Classes
{
    public interface IAuthService
    {
        public Task<TokenModel> Autenticar(AutenticarModel model);
    }
}
