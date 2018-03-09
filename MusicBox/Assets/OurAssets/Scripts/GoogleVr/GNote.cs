using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GNote : NoteObject
{
    private PhotonNote photonNote;

    void Start()
    {
        photonNote = GetComponent<PhotonNote>();
    }


    public void ClickOnNote(BaseEventData data)
    {
        NoteObject note = GvrPointerInputModule.Pointer.PointerTransform.GetComponentInChildren<NoteObject>();
        if (note)
            RemoveNote();
        Grab(null);
        //Update position on network

    }

    protected override void OnGrabbed()
    {
        Debug.Log("Grab");
        base.OnGrabbed();
        // get the Transform component of the pointer
        Transform pointerTransform = GvrPointerInputModule.Pointer.PointerTransform;
        // set the GameObject's parent to the pointer
        transform.SetParent(pointerTransform, true);

        // position it in the view
        transform.localPosition = new Vector3(0, 0, 1f);
        photonNote.TransferOwnership();
        // disable physics
        //GetComponent<Rigidbody>().isKinematic = true;
        gameObject.layer = LayerMask.NameToLayer("GvrCannotGrab");

    }

    private void RemoveNote()
    {
        NoteObject note = GvrPointerInputModule.Pointer.PointerTransform.GetComponentInChildren<NoteObject>();
        note.transform.SetParent(null);
        Destroy(note);
    }

}
