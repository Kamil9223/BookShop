using System.Net;

namespace Ksiegarnia.Exceptions
{
    public class InvalidCredentialsException : AppException
    {
        public InvalidCredentialsException(string message)
            :base(HttpStatusCode.Forbidden, AppErrorCodes.InvalidCredentialsError, message)
        { }

        public InvalidCredentialsException(int errorCode, string message)
            :base(errorCode, message)
        { }
    }
}
