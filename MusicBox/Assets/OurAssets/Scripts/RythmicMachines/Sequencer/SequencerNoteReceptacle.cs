using UnityEngine;

public class SequencerNoteReceptacle : NoteReceptacle
{
    public Material baseMaterial;

    //public Sequencer Sequencer { get; private set; }
    public VisualSequencer VisualSequencer { get; private set; }
    public int Height { get; private set; }

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
                VisualSequencer.SetNote(Height, null);
                GetComponentInChildren<MeshRenderer>().material = baseMaterial;
            }
            else
            {
                VisualSequencer.SetNote(Height, value);
                GetComponentInChildren<MeshRenderer>().material = value.GetComponent<MeshRenderer>().material;
            }
            noteHold = value;
        }
    }

    private bool initialized = false;
    public void Initialize(Sequencer sequencer, VisualSequencer vs, int height)
    {
        VisualSequencer = vs;
        //Sequencer = sequencer;
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

    public override void SwapNote(NoteObject note)
    {
        NoteHold = null;
        SetNote(note);
    }
}
