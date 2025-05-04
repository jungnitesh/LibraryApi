namespace LibraryAPI.Middleware
{
    public static class MiddleWareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
        public static IApplicationBuilder UseApiKeyMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiKeyMiddleware>();
            return app;
        }
    }
}
