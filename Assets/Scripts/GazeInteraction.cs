using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class GazeInteraction : MonoBehaviour
{
    [SerializeField] private UnityEvent onGazeSelectEnter;
    [SerializeField] private UnityEvent onGazeSelectExit;

    public void TriggerGazeEnter()
    {
        Debug.Log("Gaze interaction started!");
        onGazeSelectEnter?.Invoke();
    }

    public void TriggerGazeExit()
    {
        Debug.Log("Gaze interaction ended!");
        onGazeSelectExit?.Invoke();
    }
}
