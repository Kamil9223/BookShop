using Ksiegarnia.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Ksiegarnia.MiddleWares
{
    public class ErrorsMiddleWare : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                await HandleException(ex, context);
            }
        }

        private async Task HandleException(Exception ex, HttpContext context)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            int errorCode = 0;

            if (ex is AppException)
            {
                errorCode = ((AppException)ex).ErrorCode;
                statusCode = ((AppException)ex).StatusCode;
            }
            string result = JsonConvert.SerializeObject(new 
            { 
                errorCode = errorCode,
                errorMessage = ex.Message 
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(result);
        }
    }
}
