using UnityEngine;

namespace Chintras.Resources {
    public class Mine : Resource {
        private void Update() {
            if (occupiedChintra != null) {
                timer += Time.deltaTime;
                if (timer >= duration) {
                    Deck.instance.AddCard(BaseResourceCard.ResourceType.Rock,3);
                    ResourceFinished();
                }
            }
        }
    }
}