using reeltok.api.auth.Utils;
using reeltok.api.auth.Enums;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Interfaces.Services;

namespace reeltok.api.auth.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private ITokenValidationService _tokenValidationService;
        private ITokenGenerationService _tokenGenerationService;
        private ITokenManagementService _tokenManagementService;

        private readonly string[] _publicRoutes = new string[]
        {
            "/API/USERS/SIGNUP",
            "/API/AUTH/LOGIN",
            "/FAVICON.ICO",
            "/"
        };

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (IsPublicRoute(context.Request.Path.ToString()))
            {
                await _next(context).ConfigureAwait(false);
                return;
            }

            InitializeServices(context);

            string? accessToken = CookieUtils.GetCookieValue(context, TokenName.AccessToken);

            if (await IsAccessTokenValid(accessToken).ConfigureAwait(false))
            {
                await _next(context).ConfigureAwait(false);
                return;
            }

            string? refreshToken = CookieUtils.GetCookieValue(context, TokenName.RefreshToken);

            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new UnauthorizedAccessException("Unauthorized! Please login and try again!");
            }

            Guid userId = await GetUserIdByRefreshToken(refreshToken).ConfigureAwait(false);

            AccessToken newAccessToken = await _tokenGenerationService.GenerateAccessToken(userId)
                .ConfigureAwait(false);

            RefreshToken newRefreshToken = await _tokenGenerationService.GenerateRefreshToken(userId)
                .ConfigureAwait(false);

            await _tokenManagementService.RevokeTokens(accessToken, refreshToken).ConfigureAwait(false);

            AppendTokensToCookies(context, newAccessToken, newRefreshToken);

            await _next(context).ConfigureAwait(false);
        }

        private bool IsPublicRoute(string path)
        {
            return _publicRoutes.Contains(path.ToUpperInvariant());
        }

        private void InitializeServices(HttpContext context)
        {
            _tokenValidationService = context.RequestServices.GetRequiredService<ITokenValidationService>();
            _tokenGenerationService = context.RequestServices.GetRequiredService<ITokenGenerationService>();
            _tokenManagementService = context.RequestServices.GetRequiredService<ITokenManagementService>();
        }

        private async Task<bool> IsAccessTokenValid(string? accessToken)
        {
            return await _tokenValidationService.IsValidAccessToken(accessToken).ConfigureAwait(false);
        }

        private async Task<Guid> GetUserIdByRefreshToken(string refreshToken)
        {
            Guid? userId = await _tokenManagementService.GetUserIdByRefreshToken(refreshToken)
                .ConfigureAwait(false);

            if (!userId.HasValue)
            {
                throw new UnauthorizedAccessException("Unauthorized! Please login and try again!");
            }

            return userId.Value;
        }

        private static void AppendTokensToCookies(HttpContext context, AccessToken newAccessToken, RefreshToken newRefreshToken)
        {
            CookieUtils.AppendTokenToCookie(context, newAccessToken, TokenName.AccessToken);
            CookieUtils.AppendTokenToCookie(context, newRefreshToken, TokenName.RefreshToken);

            context.Items["NewAccessToken"] = newAccessToken.TokenValue;
        }
    }
}
