using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class ApiKeyMiddleware(RequestDelegate next)
{
    private const string ApiKeyHeaderName = "X-Api-Key";

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("API Key is missing.");
            return;
        }

        var configuredApiKey = context.RequestServices.GetService<IConfiguration>()?.GetValue<string>("ApiKey");
        if (!string.Equals(extractedApiKey, configuredApiKey))
        {
            context.Response.StatusCode = 403; // Forbidden
            await context.Response.WriteAsync("Invalid API Key.");
            return;
        }

        await next(context);
    }
}
