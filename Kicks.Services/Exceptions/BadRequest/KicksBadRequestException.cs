using Kicks.Services.Exceptions.Base;

namespace Kicks.Services.Exceptions.BadRequest
{
    public class KicksBadRequestException : KicksBaseExeption
    {
        private const int statusCode = 400;

        public KicksBadRequestException(string message) : base(statusCode, message)
        {
        }
    }
}
