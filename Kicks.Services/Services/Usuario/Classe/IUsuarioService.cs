using Kicks.Models.Usuario;

namespace Kicks.Services.Services.Usuario.Classe
{
    public interface IUsuarioService
    {
        public Task<UsuarioModel> CriarUsuario(UsuarioModel model);
        public Task<UsuarioModel> ObterById(Guid usuarioId);
        public Task<UsuarioModel> EditarUsuario(Guid usuarioId, UsuarioModel model);
        public Task<UsuarioModel> DeletarUsuario(Guid usuarioId);
    }
}
