using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;

public class mastermusiccontroller : MonoBehaviour
{
    // Global variables
    public int currentlyPlayingInstruments;
    public double maxVolume;
    public AudioMixer mixer;
    public VideoPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        currentlyPlayingInstruments = 0;
        //mixer = Resources.Load("Master") as AudioMixer;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyPlayingInstruments == 0)
        {
            player.Pause();
        } else
        {
            player.Play();
        }
    }
}
