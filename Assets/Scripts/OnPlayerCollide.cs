using UnityEngine;

public class OnPlayerCollide : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        //This has runtime of like O(fucking eternity), but I don't care
        if (other.gameObject.name == "Player")
        {
            var loggerGO = GameObject.Find("Logger");
            var logger = (Logger) loggerGO.GetComponent(typeof(Logger));
            logger.OnCollide();
        }
    }
}