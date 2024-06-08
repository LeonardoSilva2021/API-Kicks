using Kicks.Models.Enuns;

namespace Kicks.Models.Categoria
{
    public class CategoriaModel
    {
        public Guid CategoriaId { get; set; }
        public Guid UsuarioId { get; set; }
        public string? Nome { get; set; }
        public EnumTipoCategoriaModel Tipo { get; set; }
        public string? ImagemUrl { get; set; }
    }
}
