using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Chintras.Resources {
    public class Tree : Resource {
        private void Update() {
            if (occupiedChintra != null) {
                timer += Time.deltaTime;
                if (timer >= duration) {
                    ResourceManager.instance.AddWood(Random.Range(minAmount, maxAmount + 1));
                    ResourceFinished();
                }
            }
        }
    }
}
