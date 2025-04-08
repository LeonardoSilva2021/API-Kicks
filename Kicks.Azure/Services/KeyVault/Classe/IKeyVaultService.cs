namespace Kicks.Azure.Services.KeyVault.Classe
{
    public interface IKeyVaultService
    {
        public Task<string> GetSecretAsync(string secretName);
    }
}
