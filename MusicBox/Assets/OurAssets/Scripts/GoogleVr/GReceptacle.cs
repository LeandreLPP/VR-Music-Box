using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GReceptacle : NoteReceptacle
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

        //Set the note in the receptacle
        note.gameObject.transform.SetParent(transform, true);
        note.gameObject.transform.position = transform.position;

        note.gameObject.transform.gameObject.layer = LayerMask.NameToLayer("Default");
        gameObject.layer = LayerMask.NameToLayer("GvrCannotGrab");

        note.gameObject.transform.gameObject.GetComponent<GNote>().Release(null);
        note.gameObject.transform.gameObject.GetComponent<GNote>().Receptacle = this;
        return true;
    }


    public override bool RemoveNote(NoteObject note)
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        return true;
    }

    public override bool CanReceive(NoteObject note)
    {
        throw new System.NotImplementedException();
    }
}

