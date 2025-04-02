using Kicks.Data.Database;
using Kicks.Domain.Usuario;
using Kicks.Models.Auth;
using Kicks.Models.Usuario;
using Kicks.Services.Services.Auth.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kicks.Services.Services.Auth
{
    public class AuthService : IAuthService
    {
        #region Dependências
        private readonly KicksDataContext _DataContext;
        #endregion

        #region Construtor
        public AuthService(KicksDataContext dataContext)
        {
            _DataContext = dataContext;
        }
        #endregion

        #region Criar Token
        public string GenereteToken(UsuarioModel model)
        {

            var claims = new[]
            {

                new Claim("PrimeiroNomeUsuario", model.PrimeiroNome),
                new Claim("SegundoNomeUsuario", model.SegundoNome),
                new Claim("Email", model.Email),
                new Claim("Adiministrador", model.Admin.ToString()),
            };

            var key = new KeyVault();
            var secret = key.GetSecret("key-token-authentication");
            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret.Value));
            var credecials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credecials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
        #endregion

        #region Autenticar
        public async Task<TokenModel> Autenticar(AutenticarModel model)
        {
            var context = await _DataContext.Set<UsuarioEntity>()
                .Where(x => x.Email == model.Email && x.Senha == model.Senha)
                .Select(x => new UsuarioEntity()
                {
                    UsuarioId = x.UsuarioId,
                    PrimeiroNome = x.PrimeiroNome,
                    SegundoNome = x.SegundoNome,
                    Email = x.Email,
                    Senha = x.Senha,
                    Admin = Convert.ToBoolean(x.Admin),
                }).FirstOrDefaultAsync();

            if (context == null)
                throw new Exception("Email ou senha invalidos.");

            var token = GenereteToken(context);

            var response = new TokenModel()
            {
                UsuarioId = context.UsuarioId,
                Token = token,
            };

            return response;
        }
        #endregion
    }
}
