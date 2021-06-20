using UnityEngine;
using System.Collections.Generic;

public class MoveOnClick : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private CharacterController controller;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject selectionPrefab;

    private Transform target;
    private List<GameObject> toDestroy = new List<GameObject>();

    private void Update()
    {
        CheckInput();

        if (target)
        {
            Vector3 dir = target.position - transform.position;
            controller.Move(dir * speed * Time.deltaTime);
        }

        if (Time.frameCount % 10 == 0)
        {
            Clean();
        }
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (target)
                    toDestroy.Add(target.gameObject);
                target = Instantiate(selectionPrefab, hit.transform).GetComponent<Transform>();
                target.position = hit.point;
            }
        }
    }

    private void Clean()
    {
        foreach (GameObject obj in toDestroy)
            Destroy(obj);
    }
}
