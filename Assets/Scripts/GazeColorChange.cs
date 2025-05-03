using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GazeColorChange : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color originalColor;
    public Color gazeColor = Color.red; // Change to any color you want

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    public void OnGazeEnter()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = gazeColor;
        }
    }

    public void OnGazeExit()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }
    }
}
