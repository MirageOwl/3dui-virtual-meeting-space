using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 1f;
    [SerializeField] private float verticalSpeed = 1f;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    
    void Start()
    {
        // This has horrible performance, I guess it's OK in the Start method
        // cam = Camera.main;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
    }
}

