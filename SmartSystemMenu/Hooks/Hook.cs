using System;

namespace SmartSystemMenu.Hooks
{
    abstract class Hook
    {
        protected bool isActive = false;
        protected IntPtr handle;

        public bool IsActive
        {
            get { return isActive; }
        }

        public Hook(IntPtr windowHandle)
        {
            handle = windowHandle;
        }

        public void Start()
        {
            if (!isActive)
            {
                isActive = true;
                OnStart();
            }
        }

        public void Stop()
        {
            if (isActive)
            {
                OnStop();
                isActive = false;
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
