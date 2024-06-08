using Kicks.Domain.Enuns;
using Kicks.Models.Categoria;
using Kicks.Models.Enuns;

namespace Kicks.Domain.Categoria
{
    public class CategoriaEntity
    {
        public Guid CategoriaId { get; set; }
        public Guid UsuarioId { get; set; }
        public string? Nome { get; set; }
        public EnumTipoCategoriaEntity Tipo { get; set; }
        public string? ImagemUrl { get; set; }

        public static implicit operator CategoriaEntity(CategoriaModel model)
        {
            return new CategoriaEntity()
            {
                CategoriaId = model.CategoriaId,
                UsuarioId = model.UsuarioId,
                Nome = model.Nome,
                Tipo = (EnumTipoCategoriaEntity) model.Tipo,
                ImagemUrl = model.ImagemUrl,
            };
        }

        public static implicit operator CategoriaModel(CategoriaEntity entity) 
        {
            return new CategoriaModel()
            {
                CategoriaId = entity.CategoriaId,
                 UsuarioId = entity.UsuarioId,
                 Nome = entity.Nome,  
                 Tipo = (EnumTipoCategoriaModel) entity.Tipo,
                ImagemUrl = entity.ImagemUrl,
            };
        }
    }
}
