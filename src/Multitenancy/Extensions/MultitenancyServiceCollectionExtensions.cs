using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Multitenancy.Extensions
{
    public static class MultitenancyServiceCollectionExtensions
    {
        public static IServiceCollection AddMultitenancy<TTheme, TResolver>(this IServiceCollection services)
            where TResolver : class, IThemeResolver<TTheme>
            where TTheme : class
        {
            services.AddScoped<IThemeResolver<TTheme>, TResolver>();

            // No longer registered by default as of ASP.NET Core RC2
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Make Tenant and TenantContext injectable
            services.AddScoped(prov => prov.GetService<IHttpContextAccessor>()?.HttpContext?.GetThemeContext<TTheme>());
            services.AddScoped(prov => prov.GetService<ThemeContext<TTheme>>()?.Theme);

            // Make ITenant injectable for handling null injection, similar to IOptions
            services.AddScoped<ITheme<TTheme>>(prov => new ThemeWrapper<TTheme>(prov.GetService<TTheme>()));

            // Ensure caching is available for caching resolvers
            //var resolverType = typeof(TResolver);
            //if (typeof(MemoryCacheTenantResolver<TTheme>).IsAssignableFrom(resolverType))
            //{
            //    services.AddMemoryCache();
            //}

            return services;
        }
    }
}