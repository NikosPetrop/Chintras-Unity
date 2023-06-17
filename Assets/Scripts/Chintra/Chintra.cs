using System;
using UnityEngine;

public class Chintra : MonoBehaviour {
    public float Speed = 5f;

    private State state;
    private Vector3 posToGo;
    
    
    private void Start() {
        
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad0)) {
            state = State.Idle;
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            state = State.Walking;
        }
        
        switch (state) {
            case State.Idle:
                break;
            case State.Walking:
                // Todo: Use pathfinding
                var distance = Speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, posToGo, distance);
                break;
            case State.Occupied:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    //TODO: Implement Command Patern later
    public void MoveTo(Vector3 targetPos) {
        state = State.Walking;
        gameObject.SetActive(true);
        posToGo = targetPos;
    }

    public void StartTark() {
        state = State.Occupied;
        gameObject.SetActive(false);
    }

    public void EndTask() {
        state = State.Idle;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other) {
        var occupiable = other.GetComponentInParent<IOccupiable>();
        occupiable?.Occupy(this);
    }

    public State GetState() => state;
    
    public enum State {
        Idle,
        Walking,
        Occupied,
    }
}
