using Azure.Storage.Blobs;
using Kicks.Services.Exceptions.BadRequest;
using System.Text.RegularExpressions;

namespace Kicks.Services.Utils
{
    public class ConvertImageBaseToUrl
    {
        #region Validar Base64
        private static bool ValidateBase64(string input)
        {
            try
            {
                Convert.FromBase64String(input);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        #endregion

        #region Converter Imagem Base64 Em URL
        public static string ImageUrl(string imageBase64)
        {
            if (imageBase64 == string.Empty)
            {
                throw new KicksBadRequestException("A imagem não pode estar vazia.");
            }

            if (ValidateBase64(imageBase64))
            {
                throw new KicksBadRequestException("A imagem precisa ser enviada como base64.");
            }

            if (Uri.TryCreate(imageBase64, UriKind.Absolute, out Uri uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                return imageBase64;
            }

            var filename = Guid.NewGuid().ToString() + ".png";

            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(imageBase64, "");
            byte[] imageBytes = Convert.FromBase64String(data);

            var blobClient = new BlobClient("DefaultEndpointsProtocol=https;AccountName=kicksblob;AccountKey=yukP7Udm7GS0kP24mZzs22rG8yLhdI1J1nSDLI2jK6t0xhu2hkrMfZ5ce0i/RLNpw0/CIfzTlz2A+AStUvXNkA==;EndpointSuffix=core.windows.net", "demo", filename);

            using (var stream = new MemoryStream(imageBytes))
            {
                blobClient.Upload(stream);
            }

            return blobClient.Uri.AbsoluteUri;
        } 
        #endregion
    }
}
