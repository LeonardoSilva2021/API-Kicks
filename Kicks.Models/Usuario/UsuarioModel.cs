namespace Kicks.Models.Usuario
{
    public class UsuarioModel
    {
        public Guid UsuarioId { get; set; } 
        public string PrimeiroNome { get; set; } = string.Empty;
        public string SegundoNome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public bool Admin { get; set; }
    }
}
