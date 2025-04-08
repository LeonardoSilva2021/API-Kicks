using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Kicks.Azure.Services.KeyVault.Classe;
using Microsoft.Extensions.Configuration;

namespace Kicks.Azure.Services.KeyVault
{
    public class KeyVaultService : IKeyVaultService
    {
        #region Dependências 
        private readonly SecretClient _secretClient;
        #endregion

        #region Construtores
        public KeyVaultService(IConfiguration configuration)
        {
            var keyVaultUri = configuration["KEY_VAULT_URI"];

            if (string.IsNullOrWhiteSpace(keyVaultUri))
                throw new ArgumentException("A URI do Key Vault não foi definida nas variáveis de ambiente.");

            _secretClient = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());
        }

        public KeyVaultService(SecretClient secretClient)
        {
            _secretClient = secretClient;
        } 
        #endregion

        #region Get Secret Key Vault
        public async Task<string> GetSecretAsync(string secretName)
        {
            KeyVaultSecret response = await _secretClient.GetSecretAsync(secretName);

            if (response?.Value == null)
                throw new InvalidOperationException($"O segredo '{secretName}' não foi encontrado.");


            return response.Value;
        } 
        #endregion
    }
}
