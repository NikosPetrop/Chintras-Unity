using System;
using UnityEngine;

public class ChintraAnimator : MonoBehaviour {
    [SerializeField] private Animator animator;

    private Chintra chintra;
    private static readonly int state = Animator.StringToHash("State");

    private void Start() {
        chintra = GetComponent<Chintra>();
    }

    private void Update() {
        switch (chintra.GetState()) {
            case Chintra.State.Idle:
                animator.SetInteger(state, (int)Chintra.State.Idle);
                break;
            case Chintra.State.Walking:
                animator.SetInteger(state, (int)Chintra.State.Walking);
                break;
            case Chintra.State.Occupied:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
