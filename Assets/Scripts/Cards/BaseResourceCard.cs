using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseResourceCard : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler {
    public int CurrentDurability;
    public int MaxDurability;
    public ResourceType Resource;
    
    public RectTransform Transform { get; private set; }

    private void Awake() {
        Transform = GetComponent<RectTransform>();
    }

    private void Start() {
        
    }

    public enum ResourceType {
        Rock,
        Wood,
    }

    public virtual void OnPointerEnter(PointerEventData eventData) {
        Debug.Log(gameObject.name);
    }

    public virtual void OnPointerClick(PointerEventData eventData) {
        
    }
}
