using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

public class KeyVault
{
    private readonly SecretClient _secretClient;

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

    public KeyVaultSecret GetSecret(string secretName)
    {
        KeyVaultSecret secret = _secretClient.GetSecret(secretName);
        return secret;
    }
}
