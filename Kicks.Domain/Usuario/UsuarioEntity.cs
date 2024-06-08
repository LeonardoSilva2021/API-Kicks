using Kicks.Models.Usuario;

namespace Kicks.Domain.Usuario
{
    public class UsuarioEntity
    {
        public Guid UsuarioId { get; set; }
        public string PrimeiroNome { get; set; } = string.Empty;
        public string SegundoNome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public bool Admin { get; set; }

        public static implicit operator UsuarioEntity(UsuarioModel model)
        {
            return new UsuarioEntity()
            {
                UsuarioId = model.UsuarioId,
                PrimeiroNome = model.PrimeiroNome,
                SegundoNome = model.SegundoNome,
                Email = model.Email,
                Senha = model.Senha,
                Admin = model.Admin,
            };
        }

        public static implicit operator UsuarioModel(UsuarioEntity model)
        {
            return new UsuarioModel()
            {
                UsuarioId = model.UsuarioId,
                PrimeiroNome = model.PrimeiroNome,
                SegundoNome = model.SegundoNome,
                Email = model.Email,    
                Senha = model.Senha,
                Admin = model.Admin,
            };
        }
    }
}
