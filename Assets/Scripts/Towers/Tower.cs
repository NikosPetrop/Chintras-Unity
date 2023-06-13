using System;
using Chintras.Editor;
using UnityEngine;

public class Tower : MonoBehaviour, IOccupiable {
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected float fireRate;
    
    protected Chintra occupiedChintra;

    public virtual void Occupy(Chintra chintra) {
        if (!IsActive) {
            occupiedChintra = chintra;
            occupiedChintra.StartTark();
            Utils.DebugLog($"{occupiedChintra.name} interacts with {gameObject.name}");
            return;
        }
        
        // In Case of Click again - unassign chintra
        LeaveTower();
    }

    public void AssignChintra(Chintra chintra) => chintra.MoveTo(transform.position);

    protected void LeaveTower() {
        occupiedChintra.EndTask();
        occupiedChintra = null;
    }

    public bool IsActive => occupiedChintra != null;
}
