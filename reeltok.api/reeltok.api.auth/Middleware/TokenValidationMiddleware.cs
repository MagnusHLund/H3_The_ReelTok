using System.Security.Claims;
using reeltok.api.auth.Entities;
using reeltok.api.auth.Enums;
using reeltok.api.auth.Services;
using reeltok.api.auth.Utils;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokensService _tokensService;

        private readonly string[] _publicRoutes = new string[]
        {
            "api/auth/CreateUser",
            "api/auth/Login"
        };

        public TokenValidationMiddleware(RequestDelegate next, TokensService tokensService)
        {
            _next = next;
            _tokensService = tokensService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(_publicRoutes.Contains(context.Request.Path.ToString())) {
                return;
            }

            if (IsValidAccessToken(context))
            {
                await _next(context).ConfigureAwait(false);
                return;
            }

            if (IsValidRefreshToken(context))
            {
                
                Tokens tokens = GenerateTokens();

                CookieUtils.AppendTokenToCookie(context, tokens.AccessToken, TokenName.AccessToken);
                CookieUtils.AppendTokenToCookie(context, tokens.RefreshToken, TokenName.RefreshToken);

                await _next(context);
            }
            else
            {
                throw new UnauthorizedAccessException("Unauthorized! Please login and try again!");
            }
        }

        private bool IsValidAccessToken(HttpContext httpContext)
        {
            string? accessToken = CookieUtils.GetCookieValue(httpContext, TokenName.AccessToken);
            return !string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(_tokensService.DecodeAccessToken(accessToken)?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        private bool IsValidRefreshToken(HttpContext httpContext)
        {
            string? refreshToken = CookieUtils.GetCookieValue(httpContext, TokenName.RefreshToken);
            return !string.IsNullOrEmpty(refreshToken) && !string.IsNullOrEmpty(_tokensService.DecodeRefreshToken(refreshToken)?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        private Tokens GenerateTokens(Guid userId) {
            AccessToken newAccessToken = _tokensService.GenerateAccessToken(userId);
            RefreshToken newRefreshToken = _tokensService.GenerateRefreshToken(userId);

            return new Tokens (
                accessToken: newAccessToken,
                refreshToken: newRefreshToken
            );
        }
    }
}
