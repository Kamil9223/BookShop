﻿using System.Net;

namespace Ksiegarnia.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message)
            :base(HttpStatusCode.NotFound, AppErrorCodes.NotFoundError, message)
        { }

        public NotFoundException(int errorCode, string message)
            : base(errorCode, message)
        { }
    }
}