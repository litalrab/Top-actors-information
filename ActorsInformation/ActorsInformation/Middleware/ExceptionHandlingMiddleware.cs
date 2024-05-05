using ActorsInformation.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ActorsInformation
{
    public class ExceptionHandlingMiddleware
    {
        public RequestDelegate Next { get; }

        public ExceptionHandlingMiddleware(RequestDelegate next)

        {
            Next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (DuplicateRankException e)
            {
                await HandleCustomExceptionAsync(context, e);
            }
            catch (ActorNotFoundException e)
            {
                await HandleCustomExceptionAsync(context, e);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context);
            }
        }
        private static Task HandleCustomExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new { error = exception.Message };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        private static Task HandleExceptionAsync(HttpContext context)
        {
            var response = new { error = "internal server error" };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

    }
}