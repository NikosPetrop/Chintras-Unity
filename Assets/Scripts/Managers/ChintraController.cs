using System;
using Chintras.Resources;
using UnityEngine;

public class ChintraController : MonoBehaviour {
    [SerializeField] private Chintra chintra;
    
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            
        }
        
        if (Input.GetMouseButtonDown(1)) {
            GetMouseWorldPosition();
        }
    }

    private void GetMouseWorldPosition() {
        var screenPos = Input.mousePosition;

        if (Camera.main != null) {
            var ray = Camera.main.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out var hitData)) {
                var interactable = hitData.collider.GetComponentInParent<Resource>();
                if (interactable != null) { 
                    chintra.MoveTo(interactable.transform.position);
                }
            }
        }
    }
}
