using Kicks.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Kicks.Data.Database
{
    public class KicksDataContext : DbContext
    {
        public KicksDataContext() { }
        public KicksDataContext(Guid id) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 15));
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql(
                    "Server=127.0.0.1; Database=kicks_bd; Uid=root; Pwd=root",
                    serverVersion);

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ApplyConfiguration(new UsuarioMapper.Usuario());
            mb.ApplyConfiguration(new ProdutoMapper.Produto());
            mb.ApplyConfiguration(new ProdutoImagemMapper.ProdutoImagem());
            mb.ApplyConfiguration(new ProdutoTagMapper.ProdutoTag());
            mb.ApplyConfiguration(new CategoriaMapper.Categoria());
        }
    }
}
