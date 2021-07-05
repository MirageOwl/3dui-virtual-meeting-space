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
        if (Application.absoluteURL.Contains("_"))
        {
            var controlType = Application.absoluteURL.Split('_')[1];
            if (controlType.Contains("c"))
            {
                Debug.LogWarning("Using click controlled player controller");
                playerControllers[0].Activate();
            }
            else
            {
                Debug.LogWarning("Using drag controller player controller");
                playerControllers[1].Activate();
            }
        }
        else
        {
            playerControllers[0].Activate();
            Debug.LogWarning("Could not determine the movement type to use, maybe you are in a dev environment and not in the browser? Using the first one present on the player");
        }
    }
}
