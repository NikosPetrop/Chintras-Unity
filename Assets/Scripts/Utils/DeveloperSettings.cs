using UnityEngine;

namespace Chintras.Editor {
    [CreateAssetMenu(fileName = "DeveloperSettings", menuName = "Chintras/Developer Settings")]
    public class DeveloperSettings : ScriptableObject {
        public bool DebugMessages = false;
    }
}