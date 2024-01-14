using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Http;

public class TokenBlacklistMiddleware
{
    private readonly RequestDelegate _next;
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenBlacklistMiddleware(
        RequestDelegate next,
         DataContext context,
         IHttpContextAccessor httpContextAccessor
        )
    {
        _next = next;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Invoke(HttpContext context)
    {
        var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();

        if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
        {
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (IsTokenBlacklisted(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

        }
        await _next(context);
    }

    private bool IsTokenBlacklisted(string token)
    {
        return _context.BlacklistedTokens.Any(t => t.Token == token);
    }
}

