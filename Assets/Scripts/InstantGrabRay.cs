using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InstantGrabRay : UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor
{
    [SerializeField]
    private bool instantGrab = true;

    [SerializeField]
    private Vector3 holdOffset = new Vector3(0f, -0.1f, 0.1f); // Adjust this for natural holding position

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        if (instantGrab && args.interactableObject is UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable)
        {
            Transform controllerTransform = this.transform;

            // Calculate the position where the ball should appear to be held
            Vector3 targetPosition = controllerTransform.position +
                                   controllerTransform.TransformDirection(holdOffset);

            // Move the ball to the holding position
            grabInteractable.transform.position = targetPosition;

            // Optional: Align rotation with gravity and controller facing
            Vector3 upDirection = Vector3.up; // Keeps ball upright
            Vector3 forwardDirection = controllerTransform.forward;
            grabInteractable.transform.rotation = Quaternion.LookRotation(forwardDirection, upDirection);

            // Set the attach point to maintain position relative to controller
            grabInteractable.attachTransform = controllerTransform;
            grabInteractable.attachTransform.localPosition = holdOffset;
        }
    }
}