namespace reeltok.api.gateway.Middleware
{
    public class CorsMiddleware
    {
        private static readonly List<string> allowedOrigins = new List<string>
        {
            "https://reeltok.site"
        };

        private readonly RequestDelegate _next;
        private readonly ILogger<CorsMiddleware> _logger;

        public CorsMiddleware(RequestDelegate next, ILogger<CorsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        { /*
            string origin = context.Request.Headers.Origin.ToString();
            _logger.LogInformation("Received Origin header: {Origin}", origin);

            if (IsValidOrigin(origin))
            {
                _logger.LogInformation("Origin {Origin} is valid, applying CORS headers.", origin);

                context.Response.Headers.Append("Access-Control-Allow-Origin", origin);
                context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, Authorization");
                context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
            }
            else
            {
                _logger.LogWarning("Origin {Origin} is not valid. Request may be blocked.", origin);
            }
            */

            //! Traefik might be messing with the Origin header, so we'll just allow all origins for now
            context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, Authorization");
            context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");

            // Handle preflight requests for CORS
            if (context.Request.Method == HttpMethods.Options)
            {
                _logger.LogInformation("Handling preflight OPTIONS request for {Path}", context.Request.Path);
                context.Response.StatusCode = StatusCodes.Status204NoContent;
                return;
            }

            await _next(context).ConfigureAwait(false);
        }

        private static bool IsValidOrigin(string origin)
        {
            if (!string.IsNullOrEmpty(origin) && allowedOrigins.Contains(origin))
            {
                return true;
            }

            // For development purposes
            if (origin.StartsWith("http://localhost:") || origin.StartsWith("https://localhost:"))
            {
                return true;
            }

            return false;
        }
    }
}
