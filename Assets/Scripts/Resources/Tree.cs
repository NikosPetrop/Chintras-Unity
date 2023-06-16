using UnityEngine;
using Random = UnityEngine.Random;

namespace Chintras.Resources {
    public class Tree : Resource {
        private void Update() {
            if (occupiedChintra != null) {
                timer += Time.deltaTime;
                if (timer >= duration) {
                    Deck.instance.AddCard(BaseResourceCard.ResourceType.Wood,3);
                    ResourceFinished();
                }
            }
        }
    }
}
