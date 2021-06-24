using UnityEngine;

public class MoveOnClick : PlayerController
{
    [SerializeField] private GameObject selectionPrefab;

    private Transform cursor;

    protected override void Awake()
    {
        base.Awake();
        cursor = Instantiate(selectionPrefab).GetComponent<Transform>();
        cursor.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Active)
        {
            MoveCursor();
            Vector3 move = Vector3.zero;

            if (cursor && Input.GetMouseButton(0))
            {
                Vector3 dir = cursor.position - transform.position;
                if (dir.sqrMagnitude > 0.1f)
                {
                    dir = new Vector3(dir.x, 0, dir.z).normalized;
                    move += dir * speed * Time.deltaTime;
                }
            }

            HandleGravity();

            controller.Move(move + Vector3.up * verticalSpeed * Time.deltaTime);
        }
    }

    private void MoveCursor()
    {
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f, groundLayer);
        if (hit) // Show cursor when in bounds
        {
            cursor.gameObject.SetActive(true);
            cursor.position = hitInfo.point;
        }
        else // Remove the cursor when out of bounds
        {
            cursor.gameObject.SetActive(false);
        }
    }
}
