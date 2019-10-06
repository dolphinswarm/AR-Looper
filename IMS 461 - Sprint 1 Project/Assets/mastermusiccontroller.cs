using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class mastermusiccontroller : MonoBehaviour
{
    // Global variables
    public int currentlyPlayingInstruments;
    public double maxVolume;
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        currentlyPlayingInstruments = 0;
        //mixer = Resources.Load("Master") as AudioMixer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
