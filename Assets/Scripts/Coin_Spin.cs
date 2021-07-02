using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Spin : MonoBehaviour
{
    [SerializeField] private bool spinX;
    [SerializeField] private bool spinY;
    [SerializeField] private bool spinZ;
    [SerializeField] private float speed = 1f;

    void Update()
    {
        Spin();
    }

    void Spin() 
    {
        if (spinX)
            transform.Rotate(speed, 0, 0);
        if (spinY)
            transform.Rotate(0, speed, 0);
        if (spinZ)
            transform.Rotate(0, 0, speed);
    }
}
