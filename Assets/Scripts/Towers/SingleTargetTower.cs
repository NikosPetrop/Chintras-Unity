using System;
using System.Collections.Generic;
using Chintras.Editor;
using UnityEngine;

public class SingleTargetTower : Tower {
    

    private Enemy currentTarget;
    private Collider[] colliderBuffer;
    private float attackCD = 0f;

    private void Start() {
        colliderBuffer = new Collider[50];
    }

    private void Update() {
        if (IsActive) {
            ObtainTarget();
            
            if (currentTarget == null) {
                return;
            }

            // Rotate Gun

            // Shoot it
            Attack();
        }
    }

    private void ObtainTarget() {
        // First check if currentTarget is still in range
        if (currentTarget != null) {
            var currTargetDist = (currentTarget.transform.position - transform.position).magnitude;
            if (currTargetDist > range) {
                Utils.DebugLog($"{currentTarget.name} left {gameObject.name} 's range.");
                currentTarget = null;
            }
        }
        
        // Obtain Closest Target
        if (currentTarget == null) {
            currentTarget = FindClosestEnemy();
            if (currentTarget != null) {
                Utils.DebugLog($"Target is [{currentTarget.name}]");
            }
        }
    }

    private void Attack() {
        if (attackCD <= 0) {
            currentTarget.DoDamage(damage);
            attackCD = 1f / fireRate;
        }
        else {
            attackCD -= Time.deltaTime;
        }
    }

    private Enemy FindClosestEnemy() {
        var minDistance = float.MaxValue;
        Enemy closestEnemy = null;
        var enemiesFound = Physics.OverlapSphereNonAlloc(transform.position, range, colliderBuffer);
        for (int i = 0; i < enemiesFound; i++) {
            var enemy = colliderBuffer[i].GetComponent<Enemy>();
            if (enemy != null) {
                float distance = (enemy.transform.position - transform.position).sqrMagnitude;
                if (minDistance > distance) {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }
        return closestEnemy;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
