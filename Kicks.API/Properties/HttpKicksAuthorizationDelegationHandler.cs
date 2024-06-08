using System.Net.Http.Headers;

namespace Kicks.API.Properties
{
    public class HttpKicksAuthorizationDelegationHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Obter o token JWT
            var token = request.Headers.Authorization.ToString(); // Ajuste conforme necessário

            // Adicionar o token JWT ao cabeçalho de autorização
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Enviar a solicitação HTTP
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
