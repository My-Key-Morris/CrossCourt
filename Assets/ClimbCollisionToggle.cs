using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbCollisionToggle: MonoBehaviour
{
    public Collider[] climbableColliders;  // All wall colliders this climbable uses
    public Collider playerCollider;        // Player's main collider (e.g., capsule on XR Origin)

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

        interactable.selectEntered.AddListener(OnGrab);
        interactable.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnGrab);
        interactable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        foreach (var col in climbableColliders)
        {
            Physics.IgnoreCollision(playerCollider, col, true);
        }

        Debug.Log("[ClimbCollision] Disabled collision with wall while climbing");
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        foreach (var col in climbableColliders)
        {
            Physics.IgnoreCollision(playerCollider, col, false);
        }

        Debug.Log("[ClimbCollision] Re-enabled collision with wall after climbing");
    }
}
