using Kicks.Azure.Services.KeyVault;
using Kicks.Azure.Services.KeyVault.Classe;
using Kicks.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Kicks.Data.Database
{
    public class KicksDataContext : DbContext
    {
        #region Construtores
        public KicksDataContext()
        {
        }
        #endregion

        #region Configuração da String de Conexão do Banco
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var secret = KeyVaultService.GetSecret("kicks-database-string-connection");

            var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(secret));

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql(
                    secret, 
                    serverVersion,
                    mySqlOptions => mySqlOptions.EnableRetryOnFailure
                    (
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                    )
                );

            base.OnConfiguring(optionsBuilder);
        }
        #endregion

        #region Passando os Modelos e Mappers do Banco
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ApplyConfiguration(new UsuarioMapper.Usuario());
            mb.ApplyConfiguration(new ProdutoMapper.Produto());
            mb.ApplyConfiguration(new ProdutoImagemMapper.ProdutoImagem());
            mb.ApplyConfiguration(new ProdutoTagMapper.ProdutoTag());
            mb.ApplyConfiguration(new CategoriaMapper.Categoria());
        } 
        #endregion
    }
}
