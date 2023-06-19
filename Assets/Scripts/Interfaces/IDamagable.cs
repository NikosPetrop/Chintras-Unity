using UnityEngine;

public interface IDamagable {
    public void TakeDamage(float damageAmount);
    public Transform GetTransform();
}
