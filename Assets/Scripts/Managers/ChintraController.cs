using System.Collections.Generic;
using UnityEngine;

public class ChintraController : MonoBehaviour {
    [SerializeField] private Chintra chintra;
    
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            
        }
        
        if (Input.GetMouseButtonDown(1)) {
            if (GetWalkOrInteractRaycastHit(out var point,out var occupiable)) {
                chintra.MoveTo(point.GetValueOrDefault(), occupiable);
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
