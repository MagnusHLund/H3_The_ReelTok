using System.Security.Claims;
using reeltok.api.auth.Utils;
using reeltok.api.auth.Enums;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private ITokensService _tokensService;

        private readonly string[] _publicRoutes = new string[]
        {
            "/api/auth/createuser",
            "/api/auth/login",
            "/favicon.ico",
            "/"
        };

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_publicRoutes.Contains(context.Request.Path.ToString().ToLower()))
            {
                await _next(context).ConfigureAwait(false);
                return;
            }

            _tokensService = context.RequestServices.GetRequiredService<ITokensService>();
            string? accessToken = CookieUtils.GetCookieValue(context, TokenName.AccessToken);

            if (IsValidAccessToken(accessToken)) // TODO: Add exclamation mark here, when done testing
            {
                await _next(context).ConfigureAwait(false);
                return;
            }

            string? refreshToken = CookieUtils.GetCookieValue(context, TokenName.RefreshToken);

            if (!string.IsNullOrEmpty(refreshToken))
            {
                Guid userId = await _tokensService.GetUserIdByRefreshToken(refreshToken).ConfigureAwait(false);

                AccessToken newAccessToken = await _tokensService.GenerateAccessToken(userId).ConfigureAwait(false);
                RefreshToken newRefreshToken = await _tokensService.GenerateRefreshToken(userId).ConfigureAwait(false);

                await _tokensService.RevokeTokens(accessToken, refreshToken).ConfigureAwait(false);

                CookieUtils.AppendTokenToCookie(context, newAccessToken, TokenName.AccessToken);
                CookieUtils.AppendTokenToCookie(context, newRefreshToken, TokenName.RefreshToken);

                await _next(context).ConfigureAwait(false);
            }
            else
            {
                throw new UnauthorizedAccessException("Unauthorized! Please login and try again!");
            }
        }

        private bool IsValidAccessToken(string? accessTokenValue)
        {
            return !string.IsNullOrEmpty(accessTokenValue) && !string.IsNullOrEmpty(_tokensService.DecodeAccessToken(accessTokenValue)?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
