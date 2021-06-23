using UnityEngine;
using System.Collections.Generic;

public class MoveOnClick : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private bool useGravity;
    [SerializeField] private float gravity;
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private CharacterController controller;
    [SerializeField] private GameObject selectionPrefab;
    [SerializeField] private LayerMask groundLayer;

    private Transform cursor;
    private List<GameObject> toDestroy = new List<GameObject>();

    private float verticalSpeed = 0f;

    private void Awake()
    {
        cursor = Instantiate(selectionPrefab).GetComponent<Transform>();
        cursor.gameObject.SetActive(false);
    }

    private void Update()
    {
        MoveCursor();
        Vector3 move = Vector3.zero;

        if (cursor && Input.GetMouseButton(0))
        {
            Vector3 dir = cursor.position - transform.position;
            dir = new Vector3(dir.x, 0, dir.z);
            move += dir * speed * Time.deltaTime;
        }

        if (useGravity)
        {
            if (Physics.CheckSphere(transform.position, 0.2f)) 
                verticalSpeed = 0f;
            else
                Debug.Log("I am not grounded");

            verticalSpeed += gravity  * Time.deltaTime;
        }

        controller.Move(move + Vector3.up * verticalSpeed * Time.deltaTime);
    }

    private void MoveCursor()
    {
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo);
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
