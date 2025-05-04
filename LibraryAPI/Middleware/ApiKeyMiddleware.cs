using LibraryAPI.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LibraryAPI.Middleware
{
    public class ApiKeyMiddleware(RequestDelegate next)
    {
        private const string ApiKeyHeaderName = "X-Api-Key";

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                throw new ApiKeyException("Api Key is missing");
            }
            var configuredApiKey = context.RequestServices.GetService<IConfiguration>()?.GetValue<string>("ApiKey");
            if (!string.Equals(extractedApiKey, configuredApiKey))
            {
                throw new ApiKeyException("Api key mismatch");
            }

            await next(context);
        }
    }
}