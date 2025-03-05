using Azure.Security.KeyVault.Secrets;
using Kicks.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Kicks.Data.Database
{
    public class KicksDataContext : DbContext
    {

        private readonly KeyVault _keyVault;

        public KicksDataContext() { }

        public KicksDataContext(KeyVault keyVault) 
        {
           _keyVault = keyVault; 
        }

        public KicksDataContext (KeyVault keyValt, Guid id) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var teste = new KeyVault();
            var teste2 = teste.GetSecret("kicks-database-string-connection");
            KeyVaultSecret secret = _keyVault.GetSecret("kicks-database-string-connection");

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 15));

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql(secret.Value, serverVersion);

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
