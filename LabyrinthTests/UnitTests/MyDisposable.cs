using System;

namespace Tree
{
    public class MyDisposable : IDisposable
    {
        public Action Disposed;

        private bool _isDisposed;

        public virtual void Dispose()
        {
            if (!_isDisposed)
            {
                DisposeInternal();
                _isDisposed = true;
                Disposed?.Invoke();
                Disposed = null;
            }
        }

        protected virtual void DisposeInternal() {}
    }
}