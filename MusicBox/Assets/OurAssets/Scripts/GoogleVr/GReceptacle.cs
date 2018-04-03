using UnityEngine;
using UnityEngine.EventSystems;

public class GReceptacle : SequencerNoteReceptacle
{
    public void ClickOnReceptacle(BaseEventData data)
    {
 
        NoteObject note = GvrPointerInputModule.Pointer.PointerTransform.GetComponentInChildren<NoteObject>();
        if (note)
            SetNote(note);

        return;
    }


    //Set note in receptacle
    public override bool SetNote(NoteObject note)
    {
        base.SetNote(note);
        //Set the note in the receptacle
        note.gameObject.transform.SetParent(transform, true);
        note.gameObject.transform.position = transform.position;

        note.gameObject.transform.gameObject.layer = LayerMask.NameToLayer("Default");
        gameObject.layer = LayerMask.NameToLayer("GvrCannotGrab");

        note.gameObject.transform.gameObject.GetComponent<GNote>().TryRelease(null);
        note.gameObject.transform.gameObject.GetComponent<GNote>().Receptacle = GetComponent<SequencerNoteReceptacle>();
        return true;
    }


    public override bool RemoveNote(NoteObject note)
    {
        base.RemoveNote(note);
        gameObject.layer = LayerMask.NameToLayer("Default");
        return true;
    }

}

