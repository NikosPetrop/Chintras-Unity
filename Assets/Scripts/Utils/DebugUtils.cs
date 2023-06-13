using UnityEngine;


namespace Chintras.Editor {
    public static class Utils {
        public static void DebugLog(string message) {
            if (Settings.DebugMessages) {
                Debug.Log(message);
            }
        }

        private static DeveloperSettings Settings => UnityEngine.Resources.Load<DeveloperSettings>("DeveloperSettings");
    }
}