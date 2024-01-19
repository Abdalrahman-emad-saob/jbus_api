using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Validations
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(params string[] allowedRoles) : base(typeof(CustomAuthorizeFilter))
        {
            Arguments = [allowedRoles];
        }
    }

    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly string[] _allowedRoles;
        private readonly ITokenHandlerService _tokenHandlerService;

        public CustomAuthorizeFilter(
            string[] allowedRoles,
            ITokenHandlerService tokenHandlerService
            )
        {
            _allowedRoles = allowedRoles ?? throw new ArgumentNullException(nameof(allowedRoles));
            _tokenHandlerService = tokenHandlerService ?? throw new ArgumentNullException(nameof(tokenHandlerService));;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (CustomAuthorizationCheck(context, _allowedRoles))
            {

            }
            else
            {
                context.Result = new ForbidResult();
            }
        }

        private bool CustomAuthorizationCheck(AuthorizationFilterContext context, string[] allowedRoles)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if(role == "Not" || !allowedRoles.Contains(role))
                return false;
            
            return true;
        }
    }
}