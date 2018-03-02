﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sequencer))]
public class VisualSequencer : MonoBehaviour {

    public float distance = 6f;
    public float height = 0f;
    public VisualSequencerStep stepPrefab;
    public SequencerNoteReceptacle receptaclePrefab;

    private Sequencer sequencer;
    private VisualSequencerStep[] stepsVisu;
    private SequencerNoteReceptacle[] receptacles;
    private bool initialized;

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
            var pos = rot * (transform.forward * distance + transform.up * height);
            stepsVisu[i] = Instantiate<GameObject>(stepPrefab.gameObject, pos, rot, transform).GetComponent<VisualSequencerStep>();
            stepsVisu[i].Initialize(sequencer, i);
        }

        receptacles = new SequencerNoteReceptacle[StepSize];
        for (int i = 0; i < StepSize; i++)
        {
            receptacles[i] = Instantiate<GameObject>(receptaclePrefab.gameObject, transform.up * i, new Quaternion(), transform).GetComponent<SequencerNoteReceptacle>();
            receptacles[i].Initialize(sequencer, i);
        }

        initialized = true;
    }

    public void PlayVisualNextStep()
    {
        if (!initialized) return;
        sequencer.PlayNextStep();
        int i = sequencer.CurrentStep == 0 ? Steps - 1 : sequencer.CurrentStep - 1;
        stepsVisu[i].PlayStep();
    }
}
