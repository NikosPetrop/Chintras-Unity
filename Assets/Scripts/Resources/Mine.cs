using UnityEngine;

namespace Chintras.Resources {
    public class Mine : Resource {
        private void Update() {
            if (occupiedChintra != null) {
                timer += Time.deltaTime;
                if (timer >= duration) {
                    ResourceManager.instance.AddRock(Random.Range(minAmount, maxAmount + 1));
                    ResourceFinished();
                }
            }
        }
    }
}
