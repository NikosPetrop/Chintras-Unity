using System;
using Chintras.Editor;
using UnityEngine;

namespace Chintras.Resources {
    public class Resource : MonoBehaviour, IOccupiable {
        [SerializeField] protected int minAmount = 1;
        [SerializeField] protected int maxAmount = 1;
        [SerializeField] protected float duration = 1;

        protected Chintra occupiedChintra;
        protected float timer;

        public virtual void Occupy(Chintra chintra) {
            occupiedChintra = chintra;
            occupiedChintra.StartTark();
            Utils.DebugLog($"{occupiedChintra.name} interacts with {gameObject.name}");
        }

        public void AssignChintra(Chintra chintra) => chintra.MoveTo(transform.position);

        protected void ResourceFinished() {
            occupiedChintra.EndTask();
            Destroy(gameObject);
        }
    }
}