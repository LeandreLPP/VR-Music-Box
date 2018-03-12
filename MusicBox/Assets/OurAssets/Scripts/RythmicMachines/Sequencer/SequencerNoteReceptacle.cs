using UnityEngine;
using System.Collections;

public class SequencerNoteReceptacle : NoteReceptacle
{
    public Material baseMaterial;

    public Sequencer Sequencer { get; private set; }
    public int Height { get; private set; }
    private bool initialized = false;
    private NoteObject noteHold;

    public override NoteObject NoteHold
    {
        get
        {
            return noteHold;
        }

        protected set
        {
            if (!initialized)
                return;

            if (value == null)
            {
                Sequencer.Notes[Height] = null;
                GetComponentInChildren<MeshRenderer>().material = baseMaterial;

            }
            else
            {
                Sequencer.Notes[Height] = value.note;

                GetComponentInChildren<MeshRenderer>().material = value.GetComponent<MeshRenderer>().material;
            }
            noteHold = value;
        }
    }


    public void Initialize(Sequencer sequencer, int height)
    {
        Sequencer = sequencer;
        Height = height;
        initialized = true;
    }

    public override bool CanReceive(NoteObject note)
    {
        return noteHold == null;
    }

    public override bool SetNote(NoteObject note)
    {
        if (CanReceive(note))
        {
            NoteHold = note;
            return true;
        }
        else
            return false;
    }

    public override bool RemoveNote(NoteObject note)
    {
        if (note == NoteHold)
        {
            NoteHold = null;
            return true;
        }
        else
            return false;
    }
}
