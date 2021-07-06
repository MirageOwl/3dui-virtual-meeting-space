using UnityEngine;

public class OnTriggerCheckpoint : MonoBehaviour
{
    private bool isDisabled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //This has runtime of like O(fucking long), but I don't care
            var loggerGO = GameObject.Find("Logger");
            var logger = (Logger) loggerGO.GetComponent(typeof(Logger));
            logger.EnterCheckpoint();
            isDisabled = true;
            CheckpointOrder.Instance.Advance();
            this.gameObject.SetActive(false);
        }
    }
}