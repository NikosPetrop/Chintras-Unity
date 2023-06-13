using UnityEngine;

public class CameraMotion : MonoBehaviour {
    [SerializeField] private float speed = 1f;
    [SerializeField] private float smoothing = 5f;
    [SerializeField] private Vector2 range = new Vector2(100f, 100f);
    [SerializeField] private int mouseScreenBoundary;
    [SerializeField] private bool shouldUseMouseInputs;

    private Vector3 curPos, input;

    private void Awake() {
        curPos = transform.position;
    }

    private void Update() {
        if (Application.isFocused) {
            HandleInputs();
            Move();
        }
    }

    private void HandleInputs() {
        // Keyboard Inputs
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        // Mouse Inputs
        if (shouldUseMouseInputs) {
            if (x == 0) {
                if (Input.mousePosition.x > Screen.width - mouseScreenBoundary) {
                    x = 1;
                }
                else if (Input.mousePosition.x < 0 + mouseScreenBoundary) {
                    x = -1;
                }
            }

            if (y == 0) {
                if (Input.mousePosition.y > Screen.height - mouseScreenBoundary) {
                    y = 1;
                }
                else if (Input.mousePosition.y < 0 + mouseScreenBoundary) {
                    y = -1;
                }
            }
        }

        var right = transform.right * x;
        var forward = transform.forward * y;

        input = (forward + right).normalized;
    }

    private void Move() {
        var targetPos = curPos + input * speed;
        if (IsInBounds(targetPos)) {
            curPos = targetPos;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothing);
    }

    private bool IsInBounds(Vector3 pos) {
        return pos.x > -range.x && pos.x < range.x && pos.z > -range.y && pos.z > -range.y;
    }
}
