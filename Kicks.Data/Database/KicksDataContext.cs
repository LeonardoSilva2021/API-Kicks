using Azure.Security.KeyVault.Secrets;
using Kicks.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Kicks.Data.Database
{
    public class KicksDataContext : DbContext
    {

        private readonly KeyVault _keyVault;

        public KicksDataContext(KeyVault keyVault) 
        {
           _keyVault = keyVault; 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            KeyVaultSecret secret = _keyVault.GetSecret("kicks-database-string-connection");

            var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(secret.Value));

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql(
                    secret.Value, serverVersion,
                    mySqlOptions => mySqlOptions.EnableRetryOnFailure
                    (
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                    )
                );
       
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
