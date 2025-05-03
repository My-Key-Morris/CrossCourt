using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class CustomHapticScript : MonoBehaviour
{
    public Transform cube;
    public Transform goal;
    private bool picked = false;
    private bool lowhover = false;
    private bool highhover = false;
    private bool medhover = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    
    public void pickedup()
    {
        picked = true;
        StartCoroutine(ContinuousVibration());
    }
    public void hoverlow()
    {
        lowhover = true;
        StartCoroutine(ContinuousVibration());
    }
    public void hovermed()
    {
        medhover = true;
        StartCoroutine(ContinuousVibration());
    }
    public void hoverhigh()
    {
        highhover = true;
        StartCoroutine(ContinuousVibration());
    }

    public void dropped()
    {
        picked = false;
    }
    public void hoverstopped()
    {
        lowhover=false;
        medhover=false;
        highhover=false;
    }
    IEnumerator ContinuousVibration()
    {
        yield return new WaitForSeconds(.1f);

        if (lowhover)
        {
            lowamp();
        }
        if (medhover)
        {
            medamp();
        }
        if (highhover)
        {
            highamp();
        }
        if (picked)
        {
            GetComponent<HapticImpulsePlayer>().SendHapticImpulse(0.05f, 1f, 200f);
        }
        yield return new WaitForSeconds(1f);

        if (picked || lowhover || medhover || highhover)
        {
            StartCoroutine(ContinuousVibration());
        }

    }
    // Update is called once per frame

    public void lowamp()
    {
        GetComponent<HapticImpulsePlayer>().SendHapticImpulse(0.1f, 1f, 100f);
    }

    public void medamp()
    {
        GetComponent<HapticImpulsePlayer>().SendHapticImpulse(0.5f, 1f, 100f);
    }
    public void highamp()
    {
        GetComponent<HapticImpulsePlayer>().SendHapticImpulse(1f, 1f, 100f);
    }
    void Update()
    {
        
    }
}
