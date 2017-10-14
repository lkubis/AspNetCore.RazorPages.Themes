using System;
using System.Collections.Generic;

namespace Multitenancy
{
    public class ThemeContext<TTheme> : IDisposable
    {
        private bool _disposed;

        public string Id { get; } = Guid.NewGuid().ToString();
        public TTheme Theme { get; }
        public IDictionary<string, object> Properties { get; }

        public ThemeContext(TTheme theme)
        {
            if (theme == null)
                throw new ArgumentNullException(nameof(theme));

            Theme = theme;
            Properties = new Dictionary<string, object>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                foreach (var prop in Properties)
                {
                    TryDisposeProperty(prop.Value as IDisposable);
                }
                TryDisposeProperty(Theme as IDisposable);
            }

            _disposed = true;
        }
        
        private void TryDisposeProperty(IDisposable obj)
        {
            if (obj == null)
                return;

            try
            {
                obj.Dispose();
            }
            catch (ObjectDisposedException)
            {
            }
        }
    }
}