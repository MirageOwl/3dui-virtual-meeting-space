using UnityEngine;

public class OnTriggerCheckpoint : MonoBehaviour
{
    private bool isDisabled = false;
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        //This has runtime of like O(fucking long), but I don't care
        var loggerGO = GameObject.Find("Logger");
        var logger = (Logger) loggerGO.GetComponent(typeof(Logger));
        logger.EnterCheckpoint();
        isDisabled = true;
        this.gameObject.SetActive(false);
    }
}