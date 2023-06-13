using UnityEngine;

public class ChintraController : MonoBehaviour {
    [SerializeField] private Chintra chintra;
    
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            
        }
        
        if (Input.GetMouseButtonDown(1)) {
            TryToOccupyAvailableChintra();
        }
    }

    private void TryToOccupyAvailableChintra() {
        var screenPos = Input.mousePosition;

        if (Camera.main != null) {
            var ray = Camera.main.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out var hitData)) {
                var occupiable = hitData.collider.GetComponentInParent<IOccupiable>();
                occupiable?.AssignChintra(chintra);
            }
        }
    }
}
