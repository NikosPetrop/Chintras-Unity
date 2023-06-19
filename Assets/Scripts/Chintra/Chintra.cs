using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Chintra : MonoBehaviour {
    public float Speed = 5f;

    [SerializeField] private GameObject selector;

    private State state;
    private NavMeshAgent agent;
    private IOccupiable targetOccupation;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;
    }

    private void Update() {
        state = agent.hasPath ? State.Walking : State.Idle;
    }
    
    public void MoveTo(Vector3 targetPos, IOccupiable occupation = null) {
        gameObject.SetActive(true);
        var successful = agent.SetDestination(targetPos);
        if (!successful) {
            Debug.LogWarning($"{gameObject.name}'s agent was not properly set!");
            return;
        }
        targetOccupation = occupation;
    }

    public void StartTark() {
        state = State.Occupied;
        gameObject.SetActive(false);
    }

    public void EndTask() {
        state = State.Idle;
        gameObject.SetActive(true);
        targetOccupation = null;
    }

    private void OnTriggerEnter(Collider other) {
        var occupiable = other.GetComponent<IOccupiable>();
        if (occupiable != null && occupiable == targetOccupation) {
            occupiable?.Occupy(this);
        }
    }

    public void SetSelector(bool enable) => selector.SetActive(enable);
    public State GetState() => state;
    
    public enum State {
        Idle,
        Walking,
        Occupied,
    }
}
