using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour , IDamagable {
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float speed;

    [Header("Debug")]
    [SerializeField] protected Tower targetTower; // Todo: This is hardcoded for implementation of attacking
    [SerializeField] protected bool shouldAttackTower = true;
    
    private float attackCD = 0f;
    private NavMeshAgent agent;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (!shouldAttackTower) { return; }
        if (targetTower == null) { return; }
        
        var atkRange = (targetTower.transform.position - transform.position).magnitude;
        
        if (atkRange >= range) {
            var distance = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetTower.transform.position, distance);
        }
        else {
            Attack();
        }
    }
    
    private void Attack() {
        if (attackCD <= 0) {
            targetTower.TakeDamage(damage);
            attackCD = 1f / fireRate;
        }
        else {
            attackCD -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damageAmount) {
        health -= damageAmount;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    

    public Transform GetTransform() => transform;
}
