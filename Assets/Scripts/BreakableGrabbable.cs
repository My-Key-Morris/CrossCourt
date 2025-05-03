using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRGrabInteractable))]
public class BreakableGrabbable : MonoBehaviour
{
    [Range(0f, 1f)]
    public float breakChance = 0.3f; // 30% chance to break on grab
    public bool destroyOnBreak = true;
    public float breakDelay = 0f;
    public float hapticDuration = 0.1f;

    private XRGrabInteractable grabInteractable;
    private bool hasBroken = false;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.hoverEntered.AddListener(OnHover);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (hasBroken) return;

        float roll = Random.value;

        if (roll <= breakChance)
        {
            hasBroken = true;
            Debug.Log($"{gameObject.name} broke!");

            if (breakDelay > 0f)
                Invoke(nameof(Break), breakDelay);
            else
                Break();
        }
    }

    void OnHover(HoverEnterEventArgs args)
    {
        // Try to get the interactor's controller and send haptics
        if (args.interactorObject is XRBaseInputInteractor controllerInteractor)
        {
            XRBaseController controller = controllerInteractor.xrController;
            if (controller != null)
            {
                controller.SendHapticImpulse(breakChance, hapticDuration);
            }
        }
    }

    void Break()
    {
        if (destroyOnBreak)
        {
            Destroy(gameObject);
        }
        else
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            grabInteractable.enabled = false;
        }
    }
}
