using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseResourceCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler {
    public int CurrentDurability;
    public int MaxDurability;
    public ResourceType Resource;

    public RectTransform RTransform { get; private set; }
    
    [SerializeField] private Animation dotweenAnimation;

    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private bool isDragging;
    private Vector2 pointerOffset;
    private Vector2 originalLocalPointerPosition;
    private int startingIndex;

    private void Awake() {
        RTransform = GetComponent<RectTransform>();
    }

    public void SetHandPositionAndRotation(Vector3 newPosition, Quaternion newRotation) {
        startingPosition = newPosition;
        startingRotation = newRotation;
        RTransform.SetLocalPositionAndRotation(startingPosition, startingRotation);
    }

    public void Initialize(int durability) {
        MaxDurability = durability;
    }
    
    public void DrawAnimation(Vector3 overridePosition) {
        var sequence = DOTween.Sequence();
        // Positioning before Animation
        sequence.Append(RTransform.DOScale(0, 0));
        //sequence.Join(RTransform.DOMove(overridePosition, 0));
        
        // Actual animation
        sequence.Append(RTransform.DOScale(1, dotweenAnimation.Duration).SetEase(dotweenAnimation.Ease));
        // sequence.Join(RTransform.DOLocalMove(startingPosition, dotweenAnimation.Duration));
        // sequence.Join(RTransform.DOLocalRotate(startingRotation.eulerAngles, dotweenAnimation.Duration));
    }

    public virtual void OnPointerEnter(PointerEventData eventData) {
        startingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        var sequence = DOTween.Sequence();
        sequence.Append(RTransform.DOScale(dotweenAnimation.Scale, dotweenAnimation.Duration).SetEase(dotweenAnimation.Ease));
        sequence.Join(RTransform.DOLocalMoveY(dotweenAnimation.Height, dotweenAnimation.Duration).SetEase(dotweenAnimation.Ease));
        sequence.Join(RTransform.DOLocalRotate(Vector3.zero, dotweenAnimation.Duration).SetEase(dotweenAnimation.Ease));
    }

    public virtual void OnPointerExit(PointerEventData eventData) {
        transform.SetSiblingIndex(startingIndex);
        var sequence = DOTween.Sequence();
        sequence.Append(RTransform.DOScale(1f, dotweenAnimation.Duration).SetEase(dotweenAnimation.Ease));
        sequence.Join(RTransform.DOLocalMove(startingPosition, dotweenAnimation.Duration).SetEase(dotweenAnimation.Ease));
        sequence.Join(RTransform.DOLocalRotate(startingRotation.eulerAngles, dotweenAnimation.Duration).SetEase(dotweenAnimation.Ease));
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
            RTransform.SetLocalPositionAndRotation(startingPosition, startingRotation);
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (isDragging) {
            RTransform.position = Input.mousePosition;
        }
    }

    [Serializable]
    private class Animation {
        public float Scale = 1.5f;
        public float Height = 160f;
        public float Duration = 0.1f;
        public Ease Ease = Ease.Linear;
    }

    public enum ResourceType {
        Rock,
        Wood,
    }
}
