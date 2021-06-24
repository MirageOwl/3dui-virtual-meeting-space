using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float speed;
    [SerializeField] protected bool useGravity;
    [SerializeField] protected float gravity;
    [Header("References")]
    [SerializeField] protected Camera cam;
    [SerializeField] protected CharacterController controller;
    [SerializeField] protected LayerMask groundLayer;

    protected float verticalSpeed = 0f;
    
    public bool Active { get; protected set; }

    protected virtual void Awake()
    {
        Active = false;
    }

    protected void HandleGravity()
    {
        if (useGravity)
        {
            if (Physics.CheckSphere(transform.position, 0.2f))
                verticalSpeed = 0f;
            else
                Debug.Log("I am not grounded");

            verticalSpeed += gravity * Time.deltaTime;
        }
    }

    public void Activate()
    {
        foreach (PlayerController controller in gameObject.GetComponents<PlayerController>())
        {
            controller.Deactivate();
        }
        Active = true;
    }

    public void Deactivate() { Active = false; }
}
