using System.IdentityModel.Tokens.Jwt;
using API.Interfaces;

namespace API.Services
{
    public class TokenHandlerService : ITokenHandlerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenHandlerService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int TokenHandler()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();

            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                var tokenHandler = new JwtSecurityTokenHandler();
                var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken == null)
                {
                    return -1;
                }

                var nameIdClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.NameId);

                if (nameIdClaim == null || !int.TryParse(nameIdClaim.Value, out int id))
                {
                    return -1;
                }
                return id;
            }
            return -1;
        }

        public string ExtractUserRole()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();

            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                var tokenHandler = new JwtSecurityTokenHandler();
                var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken == null)
                {
                    return "Not";
                }

                var roleClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Role");

                if (roleClaim == null)
                {
                    return "Not";
                }
                return roleClaim.Value;
            }
            return "Not";
        }
    }
}