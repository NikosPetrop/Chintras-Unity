using System.Collections.Generic;
using UnityEngine;

public class ChintraController : MonoBehaviour {
    private List<Chintra> selectedChintras = new List<Chintra>(10);

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // Deselect
            foreach (var chintra in selectedChintras) {
                chintra.SetSelector(false);
            }
            selectedChintras.Clear();
            
            // Select
            var screenPos = Input.mousePosition;
            if (Camera.main != null) {
                var ray = Camera.main.ScreenPointToRay(screenPos);

                if (Physics.Raycast(ray, out var hitData)) {
                    var chintra = hitData.collider.GetComponent<Chintra>();
                    if (chintra != null) {
                        selectedChintras.Add(chintra);
                        chintra.SetSelector(true);
                    }
                }
            }
        }
        
        // Command
        if (Input.GetMouseButtonDown(1)) {
            if (GetWalkOrInteractRaycastHit(out var point,out var occupiable)) {
                foreach (var chintra in selectedChintras) {
                    chintra.MoveTo(point.GetValueOrDefault(), occupiable);
                }
            }
        }
    }

    
    private bool GetWalkOrInteractRaycastHit(out Vector3? point, out IOccupiable occupiable) {
        var screenPos = Input.mousePosition;

        if (Camera.main != null) {
            var ray = Camera.main.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out var hitInfo))  {
                point = hitInfo.point;
                occupiable = hitInfo.collider.GetComponentInParent<IOccupiable>();
                return true;
            }
        }
        point = null;
        occupiable = null;
        return false;
    }
}
