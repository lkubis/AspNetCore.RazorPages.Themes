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
            if(theme == null)
                throw new ArgumentNullException(nameof(theme));

            Theme = theme;
            Properties = new Dictionary<string, object>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                
            }

            _disposed = true;

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}