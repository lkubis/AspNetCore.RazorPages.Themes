using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Multitenancy;

namespace ThemesSample.Web.Multitenancy
{
    public class ThemeResolver : IThemeResolver<Theme>
    {
        private readonly Theme _theme;

        public ThemeResolver(IOptions<MultitenancyOptions> options)
        {
            _theme = options.Value.Theme;
            
        }
        public Task<ThemeContext<Theme>> ResolveAsync(HttpContext context)
        {
            var themeContext = new ThemeContext<Theme>(_theme);
            return Task.FromResult(themeContext);
        }
    }
}