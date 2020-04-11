﻿using System.Collections.Generic;

namespace Ksiegarnia.Contracts.Responses
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors = new List<ErrorModel>();
    }

    public class ErrorModel
    {
        public string fieldName { get; set; }
        public string Message { get; set; }
    }
}
