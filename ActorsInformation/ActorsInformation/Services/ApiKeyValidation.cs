using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ActorsInformation.Services
{
    public class ApiKeyValidation : IApiKeyValidation
    {
        private readonly IConfiguration _configuration;
        private readonly string API_KEY_NAME = "XApiKey";

        public ApiKeyValidation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValidApiKey(HttpContext httpContext)
        {
            if (string.IsNullOrWhiteSpace(httpContext.Request.Headers[API_KEY_NAME]))
            {
                return false;
            }

            string? userApiKey = httpContext.Request.Headers[API_KEY_NAME];

            if (string.IsNullOrWhiteSpace(userApiKey))
                return false;

            string? apiKey = _configuration.GetValue<string>(API_KEY_NAME);

            if (apiKey == null || apiKey != userApiKey)
                return false;

            return true;
        }
    }
}