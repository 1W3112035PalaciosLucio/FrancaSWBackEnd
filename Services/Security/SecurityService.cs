using System.Security.Claims;

namespace FrancaSW.Services.Security
{
    public class SecurityService:ISecurityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SecurityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? null;
        }
        public bool CheckUserHasroles(string[] roles)
        {
            var userRoles = (_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty).Split(",").ToList();

            return userRoles.Any() && userRoles.Any(x => roles.Contains(x));
        }
    }
}
