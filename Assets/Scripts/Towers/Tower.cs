using Chintras.Editor;
using UnityEngine;

public class Tower : MonoBehaviour, IOccupiable,IDamagable {
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
    
    protected void LeaveTower() {
        occupiedChintra.EndTask();
        occupiedChintra = null;
    }

    public bool IsActive => occupiedChintra != null;
    
    public void TakeDamage(float damageAmount) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
            NavMeshUpdater.RequestNavMeshUpdate();
        }
    }

    public Transform GetTransform() => transform;
}
