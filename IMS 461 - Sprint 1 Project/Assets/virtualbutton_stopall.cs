using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class virtualbutton_stopall : MonoBehaviour, IVirtualButtonEventHandler
{
    // Global variables and objects
    public GameObject vbutton;
    public mastermusiccontroller musicController;

    // Start is called before the first frame update
    void Start()
    {
        vbutton = GameObject.Find(this.name);
        vbutton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<mastermusiccontroller>();
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

        // Turn off all the instruments playing
        musicController.currentlyPlayingInstruments = 0;
        var buttons = GameObject.FindGameObjectsWithTag("NoteButton");
        foreach (GameObject button in buttons)
        {
            var props = button.GetComponent<virtualbutton_playnote>();
            props.isPlaying = false;
            foreach (AudioSource clip in props.loops)
            {
                clip.volume = 0;
                clip.Stop();
            }
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // Print debug message
        Debug.Log(this.name + " released");
    }
}
