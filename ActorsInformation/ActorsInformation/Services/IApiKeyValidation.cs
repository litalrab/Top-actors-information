using Microsoft.AspNetCore.Http;

namespace ActorsInformation.Services
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(HttpContext httpContext);
    }
}