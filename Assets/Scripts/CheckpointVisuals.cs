using UnityEngine;
using UnityEngine.UI;

public class CheckpointVisuals : MonoBehaviour
{
    [SerializeField] private Image floor;
    [SerializeField] private Image sign;

    [SerializeField] private Color highlightColor;
    private Color previousColor;

    public void Highlight()
    {
        previousColor = floor.color;
        floor.color = highlightColor;
        sign.color = highlightColor;
    }

    public void RemoveHighlight()
    {
        floor.color = highlightColor;
        sign.color = previousColor;
    }
}
