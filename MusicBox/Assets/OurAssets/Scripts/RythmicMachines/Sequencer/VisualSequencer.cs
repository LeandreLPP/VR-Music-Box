using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sequencer))]
public class VisualSequencer : BaseGrabable {

    public float distance = 6f;
    public float heightDistance = 0.5f;
    public VisualSequencerStep stepPrefab;
    public SequencerNoteReceptacle receptaclePrefab;

    public GameObject root;

    private Sequencer sequencer;
    private VisualSequencerStep[] stepsVisu;
    private SequencerNoteReceptacle[] receptacles;

    protected bool initialized;
    private bool paused;

    public virtual int Steps
    {
        get
        {
            return sequencer.Steps;
        }
        set
        {
            sequencer.Steps = value;
            Reinit();
        }
    }

    public virtual int StepSize
    {
        get
        {
            return sequencer.StepSize;
        }
        set
        {
            sequencer.StepSize = value;
            Reinit();
        }
    }

    public Sequencer Sequencer
    {
        get
        {
            return sequencer;
        }

        private set
        {
            sequencer = value;
        }
    }

    public VisualSequencerStep[] StepsVisu
    {
        get
        {
            return stepsVisu;
        }

        private set
        {
            stepsVisu = value;
        }
    }

    public SequencerNoteReceptacle[] Receptacles
    {
        get
        {
            return receptacles;
        }

        private set
        {
            receptacles = value;
        }
    }

    protected NoteObject[] notes;

    public void SetNote(int height, NoteObject no)
    {
        notes[height] = no;
        sequencer.Notes[height] = no != null ? no.note : null;
        foreach (var s in StepsVisu)
            s.UpdateNote();
    }

    public NoteObject GetNote(int height)
    {
        return notes[height]; 
    }

    // Use this for initialization
    protected virtual void Start()
    {
        sequencer = GetComponent<Sequencer>();
        Initialize();
    }

    private void Reinit()
    {
        initialized = false;
        foreach (var s in StepsVisu)
            Destroy(s);
        Initialize();
    }

    public virtual void Initialize()
    {
        StepsVisu = new VisualSequencerStep[Steps];
        notes = new NoteObject[StepSize];
        float angle = 360f / Steps;

        for (int i = 0; i < Steps; i++)
        {
            Quaternion rot = Quaternion.AngleAxis(angle * i, Vector3.up);
            var pos = rot * (root.transform.forward * distance);
            StepsVisu[i] = Instantiate<GameObject>(stepPrefab.gameObject, pos, rot, root.transform).GetComponent<VisualSequencerStep>();
            StepsVisu[i].Initialize(sequencer, this, i);
        }

        receptacles = new SequencerNoteReceptacle[StepSize];
        for (int i = 0; i < StepSize; i++)
        {
            var pos = root.transform.up * heightDistance * (i + 2);
            var rot = new Quaternion();
            receptacles[i] = Instantiate<GameObject>(receptaclePrefab.gameObject, pos, rot, root.transform).GetComponent<SequencerNoteReceptacle>();
            receptacles[i].Initialize(sequencer, this, i);
        }

        initialized = true;
    }

    public void PlayVisualNextStep()
    {
        if (!initialized || paused)
            return;
        sequencer.PlayNextStep();
        int i = sequencer.CurrentStep == Steps ? 0 : sequencer.CurrentStep;
        StepsVisu[i].PlayStep();
    }

    protected override void OnGrabbed()
    {
        base.OnGrabbed();
        paused = true;
        root.transform.localPosition = transform.up/2f;
        root.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    protected override void OnRelease(AGrabber grabber)
    {
        base.OnGrabbed();
        paused = false;
        transform.eulerAngles =  new Vector3(0, transform.eulerAngles.y, 0);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        root.transform.localPosition = Vector3.zero;
        root.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
