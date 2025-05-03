using UnityEngine;
using System.Collections;

public class DwellResizeChessPieces : MonoBehaviour
{
    public Vector3 targetSize = new Vector3(1f, 1f, 1f); // Default size
    public float dwellTime = 3f; // Time required to trigger resize
    private bool isGazing = false;
    private Coroutine dwellCoroutine;

    public void OnGazeEnter()
    {
        if (!isGazing)
        {
            isGazing = true;
            dwellCoroutine = StartCoroutine(DwellCountdown());
        }
    }

    public void OnGazeExit()
    {
        if (isGazing)
        {
            isGazing = false;
            if (dwellCoroutine != null)
            {
                StopCoroutine(dwellCoroutine);
            }
        }
    }

    private IEnumerator DwellCountdown()
    {
        yield return new WaitForSeconds(dwellTime);
        if (isGazing) // Ensure player is still looking
        {
            ResizePieces();
        }
    }

    private void ResizePieces()
    {
        GameObject[] chessPieces = GameObject.FindGameObjectsWithTag("ChessPiece");

        foreach (GameObject piece in chessPieces)
        {
            piece.transform.localScale = targetSize;
        }

        Debug.Log("Chess pieces resized to: " + targetSize);
    }
}
