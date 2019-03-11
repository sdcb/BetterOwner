using BetterOwner.Services.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BetterOwner.Services.CurrentUser
{
    public class IdentityCurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public IdentityCurrentUser(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public int Id => int.Parse(_userManager.GetUserId(_httpContextAccessor.HttpContext.User));
    }
}
