using Kicks.Data.Database;
using Kicks.Domain.Usuario;
using Kicks.Models.Usuario;
using Kicks.Services.Exceptions.BadRequest;
using Microsoft.EntityFrameworkCore;

namespace Kicks.Services.Services.Usuario.Classe
{
    public class UsuarioService : IUsuarioService
    {
        #region Dependencias
        private readonly KicksDataContext _dataContext;
        #endregion

        #region Construtor
        public UsuarioService(KicksDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #endregion

        #region Criar um Novo Usuário
        public async Task<UsuarioModel> CriarUsuario(UsuarioModel model)
        {
            var consulta = await _dataContext.Set<UsuarioEntity>()
                .Where(x => x.UsuarioId == model.UsuarioId && x.Email == model.Email)
                .FirstOrDefaultAsync();

            if (consulta != null)
            {
                throw new KicksBadRequestException("O usuário já exite.");
            }

            var entities = await _dataContext.AddAsync<UsuarioEntity>(model);

            _dataContext.SaveChanges();

            return entities.Entity;
        }
        #endregion

        #region Obter Usuário Por Id
        public async Task<UsuarioModel> ObterById(Guid usuarioId)
        {
            var entities =
               await _dataContext.Set<UsuarioEntity>()
                   .Where(x => x.UsuarioId == usuarioId)
                   .Select(x => new UsuarioEntity()
                   {
                       UsuarioId = x.UsuarioId,
                       PrimeiroNome = x.PrimeiroNome,
                       SegundoNome = x.SegundoNome,
                       Email = x.Email,
                       Senha = x.Senha,
                       Admin = Convert.ToBoolean(x.Admin),
                   }).FirstOrDefaultAsync();

            if (entities == null)
                throw new KicksBadRequestException("Usuário não encontrado!");

            return entities;
        }
        #endregion

        #region Editar um Usuário
        public async Task<UsuarioModel> EditarUsuario(Guid usuarioId, UsuarioModel model)
        {
            var entities = await _dataContext.Set<UsuarioEntity>()
                .Where(x => x.UsuarioId == usuarioId)
                .FirstOrDefaultAsync();

            if (entities == null) 
                throw new KicksBadRequestException("Usuario não existe.");

            entities.UsuarioId = model.UsuarioId;
            entities.PrimeiroNome = model.PrimeiroNome;
            entities.SegundoNome = model.SegundoNome;
            entities.Email = model.Email;
            entities.Senha = model.Senha;
            entities.Admin = model.Admin;

            _dataContext.Update(entities);
            _dataContext.SaveChanges();

            return entities;
        }
        #endregion

        #region Deletar Usuário
        public async Task<UsuarioModel> DeletarUsuario(Guid usuarioId)
        {
            var res = await _dataContext.Set<UsuarioEntity>()
                .Where(x => x.UsuarioId == usuarioId)
                .FirstOrDefaultAsync();

            if (res == null) throw new KicksBadRequestException("O usuário não encontrado.");

            _dataContext.Remove(res);
            _dataContext.SaveChanges();

            return res;
        }
        #endregion
    }
}
