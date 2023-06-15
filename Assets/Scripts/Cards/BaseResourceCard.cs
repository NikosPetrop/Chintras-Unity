using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseResourceCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler {
    
    
    public int CurrentDurability;
    public int MaxDurability;
    public ResourceType Resource;
    public PlayerHand playerHand;

    public RectTransform Transform { get; private set; }
    
    [SerializeField] private Animation dotweenAnimation;

    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private bool isDragging;
    private Vector2 pointerOffset;
    private Vector2 originalLocalPointerPosition;
    private int startingIndex;


    private void Awake() {
        Transform = GetComponent<RectTransform>();
        playerHand = FindObjectOfType<PlayerHand>();
    }

    public void Initialize() {
        Transform.GetLocalPositionAndRotation(out startingPosition, out startingRotation);
    }

    private void Start() { }

    public virtual void OnPointerEnter(PointerEventData eventData) {
        startingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        var sequence = DOTween.Sequence();
        sequence.Append(Transform.DOScale(dotweenAnimation.Scale, dotweenAnimation.Duration));
        sequence.Join(Transform.DOLocalMoveY(dotweenAnimation.Height, dotweenAnimation.Duration));
        sequence.Join(Transform.DOLocalRotate(Vector3.zero, dotweenAnimation.Duration));
    }

    public virtual void OnPointerExit(PointerEventData eventData) {
        transform.SetSiblingIndex(startingIndex);
        var sequence = DOTween.Sequence();
        sequence.Append(Transform.DOScale(1f, dotweenAnimation.Duration));
        sequence.Join(Transform.DOLocalMove(startingPosition, dotweenAnimation.Duration));
        sequence.Join(Transform.DOLocalRotate(startingRotation.eulerAngles, dotweenAnimation.Duration));
    }

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

    [Serializable]
    private class Animation {
        public float Scale;
        public float Height;
        public float Duration;
    }

    public enum ResourceType {
        Rock,
        Wood,
    }
}
