using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlioCarProject.Infrastructure
{
    public static class ExternalProvidersRegistrations
    {
        public static void ConfigureExternalProviders(this IServiceCollection services, IConfiguration configuration)
        {
            // Google

            //dotnet user-secrets set "Authentication:Google:ClientId" ""
            //dotnet user-secrets set "Authentication:Google:ClientSecret" ""

            if (configuration["Authentication:Google:ClientId"] != null)
            {
                services.AddAuthentication()
                    .AddGoogle(o =>
                    {
                        o.ClientId = configuration["Authentication:Google:ClientId"];
                        o.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                    });
            }

        }
    }
}