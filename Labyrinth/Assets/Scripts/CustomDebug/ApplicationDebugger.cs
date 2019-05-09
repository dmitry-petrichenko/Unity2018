using UnityEngine;

namespace Scripts.CustomDebug
{
    public static class ApplicationDebugger
    {
        public static void ThrowException(string value)
        {
            Debug.LogFormat("<color=red>"+value+"</color>");
        }
    }
}