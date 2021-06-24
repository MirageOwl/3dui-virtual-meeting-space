using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowController : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    private PlayerController[] playerControllers;

    private void Start()
    {
        playerControllers = playerObject.GetComponents<PlayerController>();
        playerControllers[0].Activate();
    }

    public void ActivateController(int i)
    {
        playerControllers[i].Activate();
    }
}
