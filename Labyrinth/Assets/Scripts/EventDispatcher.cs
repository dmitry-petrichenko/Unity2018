using System;

namespace ZScripts
{
    public class EventDispatcher
    {
        protected void DispatchEvent(Action action)
        {
            if (action != null)
            {
                action();
            }
        }
    }
}