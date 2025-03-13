namespace reeltok.api.users.Middleware
{
    public class ForwardCookiesMiddleware
    {
        private readonly RequestDelegate _next;

        public ForwardCookiesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("Cookie"))
            {
                var cookies = context.Request.Headers["Cookie"].ToString();
                context.Request.Headers.Remove("Cookie");
                context.Request.Headers.Append("Cookie", cookies);
            }

            await _next(context).ConfigureAwait(false);
        }
    }

}