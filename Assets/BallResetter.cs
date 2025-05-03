using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallResetter : MonoBehaviour
{
    [System.Serializable]
    public class BallData
    {
        public GameObject ball;
        [HideInInspector] public Vector3 originalPosition;
        [HideInInspector] public Quaternion originalRotation;
    }

    public List<BallData> ballsToReset;

    private void Start()
    {
        // Store original positions
        foreach (var ball in ballsToReset)
        {
            ball.originalPosition = ball.ball.transform.position;
            ball.originalRotation = ball.ball.transform.rotation;
        }
    }

    public void OnResetTriggered(BaseInteractionEventArgs args)
    {
        Debug.Log("[BallResetter] Reset triggered!");

        foreach (var ball in ballsToReset)
        {
            ball.ball.transform.SetPositionAndRotation(ball.originalPosition, ball.originalRotation);

            // Optional: zero out velocity if Rigidbody present
            if (ball.ball.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
