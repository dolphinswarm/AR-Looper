using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class virtualbutton_changeset : MonoBehaviour, IVirtualButtonEventHandler
{
    // Global variables and objects
    public GameObject vbutton;
    public enum LoopSet
    {
        Set1 = 0,
        Set2 = 1,
        Set3 = 2,
        Set4 = 3,
        Set5 = 4,
    }
    public LoopSet currentSet;
    public Material currentMaterial;
    public Material button1material;
    public Material button2material;
    public Material button3material;
    public Material button4material;
    public Material button5material;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize button
        vbutton = GameObject.Find(this.name);
        vbutton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        currentSet = LoopSet.Set1;
        currentMaterial = vbutton.GetComponentInChildren<Renderer>().material;
    }

    // When button pressed, change the set
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        // Print debug message
        Debug.Log(this.name + " pressed");
        Debug.Log(this.tag);

        // On tap, switch the set
        currentMaterial = GetNextMaterial();
        if (currentSet == LoopSet.Set5)
        {
            currentSet = LoopSet.Set1;
        } 
        else
        {
            currentSet += 1;
        }
        vbutton.GetComponentInChildren<Renderer>().material = currentMaterial;
        Debug.Log("Current Set: " + currentSet);

        // Set the material of the other buttons
        var buttons = GameObject.FindGameObjectsWithTag("NoteButton");
        foreach (GameObject button in buttons)
        {
            button.GetComponent<virtualbutton_playnote>().currentSet = GetCurrentSet();
            button.GetComponentInChildren<Renderer>().material = currentMaterial;
        }

        // Set materials and colors of set object
        var label = GameObject.FindGameObjectWithTag("SetLabel");
        label.GetComponent<TextMesh>().text = "Set #" + ((int)currentSet + 1);
        //label.GetComponent<TextMesh>().color = currentMaterial.color;

        var setBorders = GameObject.FindGameObjectsWithTag("SetOutline");
        foreach (GameObject border in setBorders)
        {
            border.GetComponent<Renderer>().material = currentMaterial;
        }
        
    }

    // When button released...
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log(this.name + " released");
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public int GetCurrentSet()
    {
        return (int)currentSet;
    }

    public Material GetNextMaterial()
    {
        if (currentSet == LoopSet.Set1) return button2material;
        if (currentSet == LoopSet.Set2) return button3material;
        if (currentSet == LoopSet.Set3) return button4material;
        if (currentSet == LoopSet.Set4) return button5material;
        if (currentSet == LoopSet.Set5) return button1material;
        return null;
    }
}
