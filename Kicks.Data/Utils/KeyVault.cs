using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

public class KeyVault
{
    private readonly IConfiguration _configuration;
    private readonly SecretClient _secretClient;

    public KeyVault() { }

    public KeyVault(IConfiguration configuration, SecretClient secretClient)
    {
        _configuration = configuration;
        _secretClient = secretClient;
    }

    public KeyVaultSecret GetSecret(string secretName)
    {
        var client = _configuration["keyVaultUrl:BaseUrl"];
        KeyVaultSecret secret = _secretClient.GetSecret(secretName, client);
        return secret;
    }
}
