using UnityEngine;

namespace SweetLibs
{
    public static class DebugHelpers
    {
        public static void BreakIf(bool breakOnTrue)
        {
            if (breakOnTrue)
                Debug.Break();
        }
    }
}