using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Multitenancy.Extensions;

namespace Multitenancy.Middleware
{
    public class ThemeResolutionMiddleware<TTheme>
    {
        private readonly RequestDelegate next;
        private readonly ILogger log;

        public ThemeResolutionMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.log = loggerFactory?.CreateLogger<ThemeResolutionMiddleware<TTheme>>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context, IThemeResolver<TTheme> themeResolver)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (themeResolver == null) throw new ArgumentNullException(nameof(themeResolver));

            log.LogDebug("Resolving ThemeContext using {loggerType}.", themeResolver.GetType().Name);

            var themeContext = await themeResolver.ResolveAsync(context);

            if (themeContext != null)
            {
                log.LogDebug("ThemeContext Resolved. Adding to HttpContext.");
                context.SetThemeContext(themeContext);
            }
            else
            {
                log.LogDebug("ThemeContext Not Resolved.");
            }

            await next.Invoke(context);
        }
    }
}