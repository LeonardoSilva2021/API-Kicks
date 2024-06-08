namespace Kicks.Services.Exceptions.Base
{
    public class KicksBaseExeption : Exception
    {
        public int StatusCode { get; }

        public KicksBaseExeption(int statusCode, string message) : base(message)
        {
            statusCode = StatusCode;
        }
    }
}
