using Kicks.Models.Categoria;

namespace Kicks.Services.Services.Categoria.Classe
{
    public interface ICategoriaService
    {
        public Task<CategoriaModel> CriarCategoria(CategoriaModel model);
        public Task<CategoriaModel> EditarCategotia(Guid categoriaId, CategoriaModel model);
        public Task<ICollection<CategoriaModel>> ObterCategorias();
        public Task<CategoriaModel> ObterCategoriaById(Guid categoriaId);
        public Task<CategoriaModel> DeletarCategoria(Guid categoriaId);
    }
}
