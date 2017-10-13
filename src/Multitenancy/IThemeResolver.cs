using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Multitenancy
{
    public interface IThemeResolver<TTheme>
    {
        Task<ThemeContext<TTheme>> ResolveAsync(HttpContext context);
    }
}