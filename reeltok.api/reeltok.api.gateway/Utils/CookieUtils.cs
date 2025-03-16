namespace reeltok.api.gateway.Utils
{
    public static class CookieUtils
    {
        internal static bool HasCookie(HttpContext httpContext, string cookieName)
        {
            return httpContext.Request.Cookies.ContainsKey(cookieName);
        }
    }
}