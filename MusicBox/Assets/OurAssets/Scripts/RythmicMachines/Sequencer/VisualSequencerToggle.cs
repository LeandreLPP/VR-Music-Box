using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Collider))]
public class VisualSequencerToggle : MonoBehaviour
{
    public Sequencer Sequencer { get; private set; }
    public VisualSequencerStep Step { get; private set; }
    public int Height { get; private set; }
    public bool State { get; private set; }

    public Material off;
    public Material on;

    private bool initialized = false;

    private void OnTriggerEnter(Collider other)
    {
        Toggle();
    }

    public void Initialize(Sequencer sequencer, VisualSequencerStep step, int height, bool initialState)
    {
        Sequencer = sequencer;
        Step = step;
        Height = height;
        State = initialState;
        GetComponent<MeshRenderer>().material = State ? on : off;
        initialized = true;
    }

    public void Toggle()
    {
        if (!initialized)
            return;

        State = !State;
        GetComponent<MeshRenderer>().material = State ? on : off;
        Sequencer.Partition[Step.StepNumber, Height] = State;
    }

    public void PlayToogle()
    {
    }
}
