using UnityEngine;
using UnityEngine.EventSystems;

public class DragToMove : PlayerController
{
    [Header("Drag Settings")]
    [SerializeField] private float dragSmoothing = 0.5f;

    private Vector3 dir;
    private Vector3 prevPos;
    private bool dragging = false;

    private void Update()
    {
        if (Active)
        {
            HandleGravity();

            Vector3 move = Vector3.zero;
            if (Input.GetMouseButtonDown(0))
            {
                prevPos = Input.mousePosition;
                dragging = true;
            }
            else if (Input.GetMouseButton(0) && dragging)
            {
                Vector3 newDir = Input.mousePosition - prevPos;
                // Math AAAAAAAAAAA
                // our camera situation is weird so this math gets a little out of hand
                newDir = Vector3.back * newDir.y + Vector3.left * newDir.x;
                float angle = cam.transform.localEulerAngles.y - 180;
                newDir = Quaternion.Euler(Vector3.up * angle) * newDir;
                dir = Vector3.Lerp(dir, newDir, dragSmoothing * Time.deltaTime);

                prevPos = Input.mousePosition;
                move = dir.normalized * speed * Time.deltaTime;
            }
            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                dir = Vector3.zero;
            }

            controller.Move(move + Vector3.up * verticalSpeed * Time.deltaTime);
        }
    }
}
