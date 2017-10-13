using System;
using Microsoft.AspNetCore.Builder;
using Multitenancy.Middleware;

namespace Multitenancy.Extensions
{
    public static class MultitenancyApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMultitenancy<TTenant>(this IApplicationBuilder app)
        {
            if(app == null) 
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<ThemeResolutionMiddleware<TTenant>>();
        }
    }
}