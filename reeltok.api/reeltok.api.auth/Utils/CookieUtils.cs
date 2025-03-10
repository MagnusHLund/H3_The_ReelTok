using reeltok.api.auth.Enums;
using reeltok.api.auth.Interfaces.Entities;

namespace reeltok.api.auth.Utils
{
    internal static class CookieUtils
    {
        internal static void AppendTokenToCookie(HttpContext httpContext, IToken token, TokenName cookieNameEnum)
        {
            string cookieName = cookieNameEnum.ToString();
            DateTime expireDateTime = DateTimeUtils.UnixTimeToDateTime(token.ExpiresAt);

            httpContext.Response.Cookies.Append(
                cookieName, token.TokenValue, new CookieOptions
                {
                    Expires = expireDateTime,
                    HttpOnly = true,
                    Secure = true,
                    // TODO: @MagnusHLund Experiment with SameSite and Domain, when running in docker infrastructure
                }
            );
        }

        internal static void DeleteCookie(HttpContext httpContext, TokenName cookieNameEnum)
        {
            string cookieName = cookieNameEnum.ToString();
            httpContext.Response.Cookies.Delete(cookieName);
        }

        internal static string? GetCookieValue(HttpContext httpContext, TokenName cookieNameEnum)
        {
            string cookieName = cookieNameEnum.ToString();
            return httpContext.Request.Cookies[cookieName];
        }
    }
}
