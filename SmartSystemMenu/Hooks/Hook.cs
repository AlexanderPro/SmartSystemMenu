using System;

namespace SmartSystemMenu.Hooks
{
    abstract class Hook
    {
        protected bool _isActive = false;
        protected IntPtr _handle;

        public bool IsActive
        {
            get { return _isActive; }
        }

        public Hook(IntPtr windowHandle)
        {
            _handle = windowHandle;
        }

        public void Start()
        {
            if (!_isActive)
            {
                _isActive = true;
                OnStart();
            }
        }

        public void Stop()
        {
            if (_isActive)
            {
                OnStop();
                _isActive = false;
            }
        }

        ~Hook()
        {
            Stop();
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        public abstract void ProcessWindowMessage(ref System.Windows.Forms.Message m);

        protected void RaiseEvent<T>(EventHandler<T> eventHandler, T e) where T : EventArgs
        {
            EventHandler<T> handler = eventHandler;
            if (handler != null)
            {
                handler.Invoke(this, e);
            }
        }
    }
}
