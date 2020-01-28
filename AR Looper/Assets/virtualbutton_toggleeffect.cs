using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class virtualbutton_toggleeffect : MonoBehaviour, IVirtualButtonEventHandler
{
    // Global variables and objects
    public GameObject vbutton;
    public Camera ARCamera;
    public AudioReverbFilter reverb;
    public AudioLowPassFilter lowPass;
    public AudioHighPassFilter highPass;

    // Start is called before the first frame update
    void Start()
    {
        vbutton = GameObject.Find(this.name);
        vbutton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        reverb.enabled = false;
        lowPass.enabled = false;
        highPass.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When button is pressed, start the effect
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        // Print debug message
        Debug.Log(this.name + " pressed");
        Debug.Log(this.tag);

        // On tap, switch the set
        if (this.name == "ReverbButton")
        {
            reverb.enabled = !reverb.enabled;
        }
        if (this.name == "LowPassButton")
        {
            lowPass.enabled = !lowPass.enabled;
        }
        if (this.name == "HighPassButton")
        {
            highPass.enabled = !highPass.enabled;
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // Print debug message
        Debug.Log(this.name + " released");
    }
}
