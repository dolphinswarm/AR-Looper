using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class virtualbutton_playnote : MonoBehaviour, IVirtualButtonEventHandler
{
    // Global variables and objects
    public GameObject vbutton;
    public AudioSource loop1;
    public AudioSource loop2;
    public AudioSource loop3;
    public AudioSource loop4;
    public AudioSource loop5;
    public List<AudioSource> loops;
    public int currentSet;
    public virtualbutton_changeset changeSetScript;
    public mastermusiccontroller musicController;
    public bool isPlaying;

    // Start is called before the first frame update
    // Initialize button on game startup
    public void Start()
    {
        // Initialize button
        vbutton = GameObject.Find(this.name);
        vbutton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        changeSetScript = GameObject.FindGameObjectWithTag("SwitchInstrumentButton").GetComponent<virtualbutton_changeset>();
        currentSet = changeSetScript.GetCurrentSet();

        // Initalize audio
        isPlaying = false;
        loops.Add(loop1); loops.Add(loop2); loops.Add(loop3); loops.Add(loop4); loops.Add(loop5);
        foreach (AudioSource clip in loops)
        {
            clip.volume = 0;
            clip.Stop();
            clip.loop = true;
        }
        musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<mastermusiccontroller>();
    }

    // When button pressed, play a note
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        // Print debug message
        Debug.Log(this.name + " pressed");
        Debug.Log(this.tag);

        // If loop is not playing...
        if (!isPlaying)
        {
            // If master music controller is off, turn on ALL instruments
            if (musicController.currentlyPlayingInstruments == 0)
            {
                // Get all note buttons
                var buttons = GameObject.FindGameObjectsWithTag("NoteButton");
                foreach (GameObject button in buttons)
                {
                    foreach (AudioSource clip in button.GetComponent<virtualbutton_playnote>().loops)
                    {
                        clip.volume = 0;
                        clip.Play();
                    }
                }
                GetCurrentLoopFromSet().volume = 1;
                isPlaying = true;
                musicController.currentlyPlayingInstruments++;
            }

            else
            {
                // Else, turn on the loop
                GetCurrentLoopFromSet().volume = 1;
                isPlaying = true;
                musicController.currentlyPlayingInstruments++;
            }
        }

        // Else, button is playing, so...
        else
        {
            // If the set has changed, swap the loops
            if (currentSet != GetCurrentSet())
            {
                foreach (AudioSource clip in loops)
                {
                    clip.volume = 0;
                }
                GetCurrentLoopFromSet().volume = 1;
            }
            

            // Else, stop the loop
            else
            {
                isPlaying = false;
                GetCurrentLoopFromSet().volume = 0;
                musicController.currentlyPlayingInstruments--;

                // If this is the last instrument left, stop everything
                if (musicController.currentlyPlayingInstruments == 0)
                {
                    // Get all note buttons and stop them
                    var buttons = GameObject.FindGameObjectsWithTag("NoteButton");
                    foreach (GameObject button in buttons)
                    {
                        foreach (AudioSource clip in button.GetComponent<virtualbutton_playnote>().loops)
                        {
                            clip.volume = 0;
                            clip.Stop();
                        }
                    }
                }
            }
        }
        Debug.Log("Currently Playing Instruments: " + musicController.currentlyPlayingInstruments);
    }

    // When button released, stop the note
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log(this.name + " released");
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public AudioSource GetCurrentLoopFromSet()
    {
        if (currentSet == 0) return loop1;
        if (currentSet == 1) return loop2;
        if (currentSet == 2) return loop3;
        if (currentSet == 3) return loop4;
        if (currentSet == 4) return loop5;
        return null;
    }

    public int GetCurrentSet()
    {
        if (loop1.volume == 1) return 0;
        if (loop2.volume == 1) return 1;
        if (loop3.volume == 1) return 2;
        if (loop4.volume == 1) return 3;
        if (loop5.volume == 1) return 4;
        return -1;
    }

}
