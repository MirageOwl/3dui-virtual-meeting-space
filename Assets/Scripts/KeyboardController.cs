using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] private CharacterController controller; 
    [SerializeField] private float speed = 5.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Using the old input system? We might want to change that.
        // Get direction relative to look direction by getting it from the transform.
        Vector3 moveInput = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        controller.Move(moveInput * speed * Time.deltaTime);

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
    }

}
