using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointOrder : MonoBehaviour
{
    public static CheckpointOrder Instance;

    [SerializeField] Logger logger;
    [SerializeField] Collider[] checkpoints;

    private int current;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (Collider collider in checkpoints)
        {
            collider.enabled = false;
        }
        current = 0;
        checkpoints[current].enabled = true;
    }

    public void Advance()
    {
        checkpoints[current].enabled = false;
        if (current + 1 < checkpoints.Length)
        {
            current += 1;
            checkpoints[current].enabled = true;
        }
        else
        {
            logger.OnEndLogging();
        }
    }
}
