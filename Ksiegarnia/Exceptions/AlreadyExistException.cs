﻿using System.Net;

namespace Ksiegarnia.Exceptions
{
    public class AlreadyExistException : AppException
    {
        public AlreadyExistException(string message)
            :base(HttpStatusCode.Conflict, AppErrorCodes.AlreadyExistError, message)
        { }

        public AlreadyExistException(int errorCode, string message)
            :base(errorCode, message)
        { }
    }
}