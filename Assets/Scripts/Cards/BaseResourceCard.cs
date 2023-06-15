using UnityEngine;
using UnityEngine.EventSystems;

public class BaseResourceCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler {
    public int CurrentDurability;
    public int MaxDurability;
    public ResourceType Resource;

    public RectTransform Transform { get; private set; }

    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private bool isDragging;
    private Vector2 pointerOffset;
    private Vector2 originalLocalPointerPosition;


    private void Awake() {
        Transform = GetComponent<RectTransform>();
    }

    public void Initialize() {
        Transform.GetLocalPositionAndRotation(out startingPosition, out startingRotation);
    }

    private void Start() { }

    public virtual void OnPointerEnter(PointerEventData eventData) { }

    public virtual void OnPointerExit(PointerEventData eventData) { }

    public virtual void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            Debug.Log($"Inspect {gameObject.name}");
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            isDragging = true;
        }
    }
    
    public virtual void OnPointerUp(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            isDragging = false;
            Transform.SetLocalPositionAndRotation(startingPosition, startingRotation);
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (isDragging) {
            Transform.position = Input.mousePosition;
        }
    }

    public enum ResourceType {
        Rock,
        Wood,
    }
}
