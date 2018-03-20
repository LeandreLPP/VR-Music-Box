using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Collider))]
public class VisualSequencerToggle : MonoBehaviour
{
    public Sequencer Sequencer { get; protected set; }
    public VisualSequencerStep Step { get; protected set; }
    public int Height { get; protected set; }
    public bool State { get; protected set; }

    public Material off;
    public Material on;

    protected Mesh meshInitial;
    protected MeshFilter filter;
    protected new MeshRenderer renderer;

    protected bool initialized = false;

    private void Start()
    {
        filter = GetComponent<MeshFilter>();
        renderer = GetComponent<MeshRenderer>();
        meshInitial = filter.mesh;
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        Toggle();
    }

    public virtual void Initialize(Sequencer sequencer, VisualSequencerStep step, int height, bool initialState)
    {
        filter = GetComponent<MeshFilter>();
        renderer = GetComponent<MeshRenderer>();
        meshInitial = filter.mesh;
        Sequencer = sequencer;
        Step = step;
        Height = height;
        State = initialState;
        renderer.material = State ? on : off;
        initialized = true;
    }

    public void Toggle()
    {
        if (!initialized)
            return;

        State = !State;
        var note = Step.VisualSequencer.GetNote(Height);
        var onMat = note == null ? on : note.GetComponent<MeshRenderer>().material;
        renderer.material = State ? onMat : off;
        Sequencer.Partition[Step.StepNumber, Height] = State;
    }

    public void UpdateNote()
    {
        var note = Step.VisualSequencer.GetNote(Height);
        if (note == null)
        {
            renderer.material = State ? on : off;
            filter.mesh = meshInitial;
            transform.localEulerAngles = Vector3.zero;
        }
        else
        {
            renderer.material = State ? note.GetComponent<MeshRenderer>().material : off;
            filter.mesh = note.GetComponent<MeshFilter>().mesh;
            transform.localEulerAngles = note.preferedEuler;
        }
    }

    public void PlayToogle()
    {
    }
}
