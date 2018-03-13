using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GNote : NoteObject
{

    void Start()
    {
    }


    public void ClickOnNote(BaseEventData data)
    {
        NoteObject note = GvrPointerInputModule.Pointer.PointerTransform.GetComponentInChildren<NoteObject>();
        if (note)
            RemoveNote();
        Grab(null);
    }

    protected override void OnGrabbed()
    {
        base.OnGrabbed();
#if UNITY_ANDROID
        // get the Transform component of the pointer
        Transform pointerTransform = GvrPointerInputModule.Pointer.PointerTransform;
        // set the GameObject's parent to the pointer
        transform.SetParent(pointerTransform, true);

        // position it in the view
        transform.localPosition = new Vector3(0, 0, 0.5f);
        GetComponent<PhotonNote>().TransferOwnership();

        gameObject.layer = LayerMask.NameToLayer("GvrCannotGrab");
#endif

    }

    private void RemoveNote()
    {
        NoteObject note = GvrPointerInputModule.Pointer.PointerTransform.GetComponentInChildren<NoteObject>();
        note.transform.SetParent(null);
        Destroy(note);
    }

}
