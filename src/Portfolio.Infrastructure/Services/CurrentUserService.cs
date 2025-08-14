

using Microsoft.AspNetCore.Http;
using Portfolio.Application.Interfaces;
using System.Security.Claims;

namespace Portfolio.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetCurrentUserName()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context == null || context.User == null)
                return "System";

            var userName = context.User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrWhiteSpace(userName))
                return "System";

            return userName;
        }
    }
}
