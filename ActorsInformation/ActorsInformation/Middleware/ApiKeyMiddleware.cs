using ActorsInformation.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ActorsInformation.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiKeyValidation _apiKeyValidation;

        public ApiKeyMiddleware(RequestDelegate next, IApiKeyValidation apiKeyValidation)
        {
            _next = next;
            _apiKeyValidation = apiKeyValidation;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!_apiKeyValidation.IsValidApiKey(context!))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            await _next(context);
        }
    }
}
