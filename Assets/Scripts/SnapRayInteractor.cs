using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapRayInteractor : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rayInteractor;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable currentInteractable;

    void Start()
    {
        rayInteractor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor>();
        rayInteractor.selectEntered.AddListener(OnGrab); // Listen for grab event
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        currentInteractable = args.interactableObject as UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable;
        if (currentInteractable != null)
        {
            SnapToController(currentInteractable.transform);
        }
    }

    void SnapToController(Transform objectTransform)
    {
        // Snap object to controller's position and rotation
        objectTransform.position = transform.position;
        objectTransform.rotation = transform.rotation;

        // Optional: Offset if you want it slightly ahead or to the side
        // objectTransform.position += transform.forward * 0.1f;
    }

    void OnDestroy()
    {
        rayInteractor.selectEntered.RemoveListener(OnGrab); // Clean up
    }
}