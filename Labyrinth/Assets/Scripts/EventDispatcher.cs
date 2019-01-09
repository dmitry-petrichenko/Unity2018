using System;

namespace Scripts
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