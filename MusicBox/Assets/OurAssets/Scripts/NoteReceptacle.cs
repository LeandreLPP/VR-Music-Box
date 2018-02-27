using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteReceptacle : MonoBehaviour {

    public NoteObject noteHold;
    public bool Free
    {
        get
        {
            return noteHold == null;
        }
    }

    public int Step { get { return step.StepNumber; } }

    private bool initialized = false;
    private SequencerStep step;

    public void Initialized(SequencerStep step)
    {
        this.step = step;
        initialized = true;
    }

    public void SetSoundObject(NoteObject note)
    {
        noteHold = note;
        note.SetReceptacle(this);
    }

    public void RemoveSoundObject()
    {
        noteHold.SetReceptacle(null);
        noteHold = null;
    }



}
