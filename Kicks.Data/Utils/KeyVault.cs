using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

public class KeyVault
{
    #region Dependências 
    private readonly SecretClient _secretClient;
    #endregion

    #region Construtores
    public KeyVault()
    {
        var key = Environment.GetEnvironmentVariable("KEY_VAULT_URI") ?? "";


        var client = new SecretClient(new Uri(key), new DefaultAzureCredential());
        _secretClient = client;
    }

    public KeyVault(SecretClient secretClient)
    {
        _secretClient = secretClient;
    }
    #endregion

    #region Buscar Chaves do Key Vault
    public KeyVaultSecret GetSecret(string secretName)
    {
        KeyVaultSecret secret = _secretClient.GetSecret(secretName);
        return secret;
    } 
    #endregion
}
