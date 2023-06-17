using System;
using UnityEngine;

public class TowerAnimator : MonoBehaviour {
    [SerializeField] private Animator animator;

    private Tower tower;
    private static readonly int isOccupied = Animator.StringToHash("IsOccupied");

    private void Start() {
        tower = GetComponent<Tower>();
    }

    private void Update() {
        animator.SetBool(isOccupied, tower.IsActive);
    }
}
