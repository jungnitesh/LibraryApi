using LibraryAPI.Application.Dtos;
using LibraryAPI.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace LibraryAPI.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ApiKeyException ex)
            {
                logger.LogError(ex, "Api Key error occurred.");
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                await WriteErrorResponseAsync(context, (int)HttpStatusCode.Unauthorized, "Unauthorized access.", ex.Message);
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex, "Resource not found.");
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";
                await WriteErrorResponseAsync(context, (int)HttpStatusCode.NotFound, "Resource not found.", ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                logger.LogError(ex, "Unauthorized access.");
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                await WriteErrorResponseAsync(context, (int)HttpStatusCode.Unauthorized, "Unauthorized access.", ex.Message);
            }
            catch (DatabaseException ex)
            {
                logger.LogError(ex, "Database error occurred.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await WriteErrorResponseAsync(context, (int)HttpStatusCode.InternalServerError, "Database error occurred.", ex.Message);
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "non defined error");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await WriteErrorResponseAsync(context, (int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.", ex.Message);
            }
           
        }
        private static async Task WriteErrorResponseAsync(HttpContext context, int statusCode, string message, string details)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponseDto(message, details, statusCode);
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}