using reeltok.api.auth.Enums;
using reeltok.api.auth.Interfaces;

namespace reeltok.api.auth.Utils
{
    internal static class CookieUtils
    {
        internal static void AppendTokenToCookie(HttpContext httpContext, IToken token, TokenName cookieNameEnum)
        {
            string cookieName = cookieNameEnum.ToString();

            httpContext.Response.Cookies.Append(
                cookieName, token.Token, new CookieOptions
                {
                    Expires = token.ExpireDate,
                    HttpOnly = true,
                    Secure = true
                }
            );
        }

        internal static void DeleteCookie(HttpContext httpContext, TokenName cookieNameEnum)
        {
            string cookieName = cookieNameEnum.ToString();
            httpContext.Response.Cookies.Delete(cookieName);
        }

        internal static string? GetCookieValue(HttpContext httpContext, TokenName cookieNameEnum) {
            string cookieName = cookieNameEnum.ToString();
            return httpContext.Request.Cookies[cookieName];
        }
    }
}
