using BetterOwner.Services.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sdcb.AspNetCore.Authentication.YeluCasSso;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BetterOwner.Services.Identity
{
    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        internal static async Task OnCreatingClaims(HttpContext httpContext, ClaimsIdentity claimsIdentity)
        {
            var userManager = httpContext.RequestServices.GetRequiredService<UserManager>();
            
            var userName = claimsIdentity.FindFirst(CasConstants.Name).Value;
            var email = claimsIdentity.FindFirst(CasConstants.Email).Value;
            var jobId = claimsIdentity.FindFirst(CasConstants.JobNumber).Value;
            var phone = claimsIdentity.FindFirst(CasConstants.Phone).Value;
            var sex = claimsIdentity.FindFirst(CasConstants.Gender).Value;

            User systemUser = await userManager.FindByEmailAsync(email);
            if (systemUser == null)
            {
                await userManager.CreateAsync(new User
                {
                    UserName = userName,
                    Email = email,
                    OAUser = new OAUser
                    {
                        JobId = jobId, 
                        Phone = phone, 
                        Sex = (Sex)int.Parse(sex), 
                    }, 
                });
                systemUser = await userManager.FindByEmailAsync(email);
            }

            IList<string> roles = await userManager.GetRolesAsync(systemUser);

            // commit
            claimsIdentity.AddClaim(new Claim(claimsIdentity.NameClaimType, systemUser.Id.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, systemUser.Id.ToString()));
            foreach (var role in roles)
            {
                claimsIdentity.AddClaim(new Claim(claimsIdentity.RoleClaimType, role));
            }
        }
    }
}
