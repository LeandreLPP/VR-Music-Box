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
    private bool initialized;
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


    // Use this for initialization
    void Start()
    {
        sequencer = GetComponent<Sequencer>();
        Initialize();
    }

    private void Reinit()
    {
        initialized = false;
        foreach (var s in stepsVisu)
            Destroy(s);
        Initialize();
    }

    private void Initialize()
    {
        stepsVisu = new VisualSequencerStep[Steps];
        float angle = 360f / Steps;

        for (int i = 0; i < Steps; i++)
        {
            Quaternion rot = Quaternion.AngleAxis(angle * i, Vector3.up);
            var pos = rot * (root.transform.forward * distance);
            stepsVisu[i] = Instantiate<GameObject>(stepPrefab.gameObject, pos, rot, root.transform).GetComponent<VisualSequencerStep>();
            stepsVisu[i].Initialize(sequencer, i);
        }

        receptacles = new SequencerNoteReceptacle[StepSize];
        for (int i = 0; i < StepSize; i++)
        {
            var pos = root.transform.up * heightDistance * (i + 2);
            var rot = new Quaternion();
            receptacles[i] = Instantiate<GameObject>(receptaclePrefab.gameObject, pos, rot, root.transform).GetComponent<SequencerNoteReceptacle>();
            receptacles[i].Initialize(sequencer, i);
        }

        initialized = true;
    }

    public void PlayVisualNextStep()
    {
        if (!initialized || paused)
            return;
        sequencer.PlayNextStep();
        int i = sequencer.CurrentStep == Steps ? 0 : sequencer.CurrentStep;
        stepsVisu[i].PlayStep();
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
