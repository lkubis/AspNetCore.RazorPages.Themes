using System;
using Microsoft.AspNetCore.Http;

namespace Multitenancy.Extensions
{
    public static class MultitenancyHttpContextExtensions
    {
        private const string ThemeContextKey = "ThemeContext";

        public static void SetThemeContext<TTheme>(this HttpContext context, ThemeContext<TTheme> themeContext)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (themeContext == null)
                throw new ArgumentNullException(nameof(themeContext));

            context.Items[ThemeContextKey] = themeContext;
        }

        public static ThemeContext<TTheme> GetThemeContext<TTheme>(this HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            object themeContext;
            if (context.Items.TryGetValue(ThemeContextKey, out themeContext))
            {
                return themeContext as ThemeContext<TTheme>;
            }

            return null;
        }

        public static TTheme GetTheme<TTheme>(this HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var themeContext = GetThemeContext<TTheme>(context);

            if (themeContext != null)
            {
                return themeContext.Theme;
            }

            return default(TTheme);
        }
    }
}