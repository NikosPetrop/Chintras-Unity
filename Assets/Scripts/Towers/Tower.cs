using UnityEngine;

public class Tower : MonoBehaviour, IOccupiable {
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    
    protected Chintra occupiedChintra;
    
    public virtual void Occupy(Chintra chintra) {
        occupiedChintra = chintra;
        occupiedChintra.StartTark();
        Debug.Log($"{occupiedChintra.name} interacts with {gameObject.name}");
    }
    
    protected void TowerDestroyed() {
        occupiedChintra.EndTask();
        Destroy(gameObject);
    }
}
