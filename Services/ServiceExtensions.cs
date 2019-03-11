using BetterOwner.Services.CurrentUser;
using BetterOwner.Services.FileStore;
using BetterOwner.Services.TreasureManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterOwner.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddFileStore(configuration["FileStore"])
                .AddTransient<ICurrentUser, IdentityCurrentUser>()
                .AddTransient<ITreasureManager, EFTreasureManager>();
        }
    }
}
