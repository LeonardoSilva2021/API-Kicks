using Kicks.Data.Database;
using Kicks.Domain.Categoria;
using Kicks.Domain.Enuns;
using Kicks.Models.Categoria;
using Kicks.Models.Enuns;
using Kicks.Services.Exceptions.BadRequest;
using Kicks.Services.Utils;
using Microsoft.EntityFrameworkCore;

namespace Kicks.Services.Services.Categoria.Classe
{
    public class CategoriaService : ICategoriaService
    {
        #region Dependências
        private readonly KicksDataContext _kicksDataContext;
        #endregion

        #region Construtor
        public CategoriaService(KicksDataContext kicksDataContext)
        {
            _kicksDataContext = kicksDataContext;
        }
        #endregion

        #region Adicionar Categoria
        public async Task<CategoriaModel> CriarCategoria(CategoriaModel model)
        {
            var consulta = await _kicksDataContext.Set<CategoriaEntity>()
                .Where(x => x.UsuarioId == model.UsuarioId)
                .FirstOrDefaultAsync();

            if (consulta != null)
            {
                throw new KicksBadRequestException("Essa categoria já existe.");
            }

            model.ImagemUrl = ConvertImageBaseToUrl.ImageUrl(model.ImagemUrl);

            var res = await _kicksDataContext.AddAsync<CategoriaEntity>(model);

            _kicksDataContext.SaveChanges();

            return res.Entity;
        }
        #endregion

        #region Editar Categoria
        public async Task<CategoriaModel> EditarCategotia(Guid categoriaId, CategoriaModel model)
        {
            var categoria = await _kicksDataContext.Set<CategoriaEntity>()
                .Where(x => x.CategoriaId == categoriaId)
                .FirstOrDefaultAsync();

            if (categoria == null)
            {
                throw new KicksBadRequestException("A categoria não foi encontrada.");
            }

            categoria.CategoriaId= categoriaId;
            categoria.UsuarioId = model.UsuarioId;
            categoria.Nome = model.Nome;
            categoria.Tipo = (EnumTipoCategoriaEntity) model.Tipo;
            categoria.ImagemUrl = ConvertImageBaseToUrl.ImageUrl(model.ImagemUrl ?? "");

            _kicksDataContext.Update<CategoriaEntity>(categoria);

            _kicksDataContext.SaveChanges();

            return categoria;
        }
        #endregion

        #region Obetr Todas As Categorias
        public async Task<ICollection<CategoriaModel>> ObterCategorias()
        {
            var consulta = await _kicksDataContext.Set<CategoriaEntity>().ToListAsync();

            var categoria = consulta.Select(x => new CategoriaModel()
            {
                CategoriaId = x.CategoriaId,
                UsuarioId = x.UsuarioId,
                ImagemUrl = x.ImagemUrl,
                Nome = x.Nome,
                Tipo = (EnumTipoCategoriaModel)x.Tipo,
            }).ToList();

            return categoria;
        }
        #endregion

        #region Obter Categoria Por Id
        public async Task<CategoriaModel> ObterCategoriaById(Guid categoriaId)
        {
            var consulta = await _kicksDataContext.Set<CategoriaEntity>()
                .Where(x => x.CategoriaId == categoriaId)
                .FirstOrDefaultAsync();

            if (consulta == null)
            {
                throw new KicksBadRequestException("Nenhuma categoria foi encontrada");
            }

            return consulta;
        }
        #endregion

        #region Deletar Categoria
        public async Task<CategoriaModel> DeletarCategoria(Guid categoriaId)
        {
            var consulta = await _kicksDataContext.Set<CategoriaEntity>()
                .Where(x => x.CategoriaId == categoriaId)
                .FirstOrDefaultAsync();

            if (consulta == null)
            {
                throw new KicksBadRequestException("A categoria não foi encontrada.");
            }

            _kicksDataContext.Remove(consulta);

            _kicksDataContext.SaveChanges();

            return consulta;
        } 
        #endregion
    }
}
