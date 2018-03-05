using UnityEngine;
using System.Collections;

public class SequencerNoteReceptacle : NoteReceptacle
{
    public Sequencer Sequencer { get; private set; }
    public int Height { get; private set; }

    private NoteObject noteHold;
    public NoteObject NoteHold
    {
        get
        {
            return noteHold;
        }

        private set
        {
            if (!initialized)
                return;

            if (value == null)
                Sequencer.Notes[Height] = null;
            else
                Sequencer.Notes[Height] = value.note;

            noteHold = value;
        }
    }

    private bool initialized = false;
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
