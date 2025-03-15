namespace reeltok.api.gateway.Middleware
{
    public class CorsMiddleware
    {
        private readonly RequestDelegate _next;

        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            List<string> allowedOrigins = new List<string>
            {
                "http://localhost",
                "https://localhost",
                "https://reeltok.site"
            };

            string origin = context.Request.Headers.Origin.ToString();
            if (!string.IsNullOrEmpty(origin) && allowedOrigins.Contains(origin))
            {
                context.Response.Headers.Append("Access-Control-Allow-Origin", origin);
                context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, Authorization");
                context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
            }

            // Handle preflight requests for CORS
            if (context.Request.Method == HttpMethods.Options)
            {
                context.Response.StatusCode = StatusCodes.Status204NoContent;
                return;
            }

            await _next(context).ConfigureAwait(false);
        }
    }
}
