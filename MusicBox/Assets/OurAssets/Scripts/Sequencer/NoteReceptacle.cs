using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteReceptacle : MonoBehaviour {

    #region Settings
    public Material baseMaterial;
    public Material containsNoteMaterial;
    public Material allowInsertMaterial;
    #endregion

    #region Protected fields
    private bool initialized = false;
    private SequencerStep step;
    #endregion

    private NoteObject noteHold;
    public NoteObject NoteHold
    {
        get
        {
            return noteHold;
        }
        set
        {
            noteHold = value;
            if(noteHold)
                GetComponent<MeshRenderer>().material = containsNoteMaterial;
            else
                GetComponent<MeshRenderer>().material = baseMaterial;
        }
    }

    public bool Free
    {
        get
        {
            return NoteHold == null;
        }
    }

    public int Step { get { return step.StepNumber; } }

    public void SetSoundObject(NoteObject note)
    {
        if (!initialized) return;

        NoteHold = note;
        note.SetReceptacle(this);
    }

    public void RemoveSoundObject()
    {
        if (!initialized) return;

        NoteHold.SetReceptacle(null);
        NoteHold = null;
    }

    public void Initialized(SequencerStep step)
    {
        this.step = step;
        initialized = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var noteObject = other.GetComponent<NoteObject>();
        if (noteObject != null)
        {
            if(step.HasFreeSpot)
                GetComponent<MeshRenderer>().material = allowInsertMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var noteObject = other.GetComponent<NoteObject>();
        if (noteObject != null)
        {
            if(noteHold != null)
                GetComponent<MeshRenderer>().material = containsNoteMaterial;
            else
                GetComponent<MeshRenderer>().material = baseMaterial;
        }
    }
}
