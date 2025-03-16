namespace reeltok.api.gateway.Middleware
{
    public class CorsMiddleware
    {
        private static readonly List<string> allowedOrigins = new List<string>
        {
            "https://reeltok.site"
        };

        private readonly RequestDelegate _next;

        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Origin header: " + context.Request.Headers.Origin.ToString());
            string origin = context.Request.Headers.Origin.ToString();

            if (IsValidOrigin(origin))
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

        private static bool IsValidOrigin(string origin)
        {
            if (!string.IsNullOrEmpty(origin) && allowedOrigins.Contains(origin))
            {
                Console.WriteLine("Origin is allowed by allowedOrigins array");
                return true;
            }

            // For development purposes
            if (origin.StartsWith("http://localhost:") || origin.StartsWith("https://localhost:"))
            {
                Console.WriteLine("Origin is allowed because it is localhost");
                return true;
            }

            Console.WriteLine("Origin is not allowed");
            return false;
        }
    }
}
