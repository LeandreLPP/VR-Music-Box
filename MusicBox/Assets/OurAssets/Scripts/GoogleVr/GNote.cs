using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GNote : NoteObject
{

    public override bool IsGrabbed
    {
        get
        {
            return isGrabbed;
        }
        set
        {
            isGrabbed = value;
            PhotonView photonView = GetComponent<PhotonView>();
            if (photonView && photonView.isMine)
                photonView.RPC("UpdateIsGrabbed", PhotonTargets.OthersBuffered);
        }
    }

    void Start()
    {
    }


    public void ClickOnNote(BaseEventData data)
    {
        if (IsGrabbed)
            return;
        NoteObject note = GvrPointerInputModule.Pointer.PointerTransform.GetComponentInChildren<NoteObject>();
        var photonNote = GetComponent<PhotonNote>();
        if (photonNote)
            photonNote.TransferOwnership();
        if (note && Receptacle)
            SwapNotes(note);
        else if (note)
            RemoveNote();
        Grab(null);
            
    }

    protected override void OnGrabbed()
    {
#if UNITY_ANDROID
        if(Receptacle)
            Receptacle.gameObject.layer = LayerMask.NameToLayer("Default");
#endif

        base.OnGrabbed();
#if UNITY_ANDROID
        // get the Transform component of the pointer
        Transform pointerTransform = GvrPointerInputModule.Pointer.PointerTransform;
        // set the GameObject's parent to the pointer
        transform.SetParent(pointerTransform, true);

        // position it in the view
        transform.localPosition = new Vector3(0, 0, 0.5f);

        gameObject.layer = LayerMask.NameToLayer("GvrCannotGrab");
        GetComponent<PhotonNote>().TransferOwnership();
#endif
    }

    private void RemoveNote()
    {
        NoteObject note = GvrPointerInputModule.Pointer.PointerTransform.GetComponentInChildren<NoteObject>();
        note.transform.SetParent(null);
        PhotonNetwork.Destroy(note.GetComponent<PhotonView>());
    }

    //Click on a note which is in a receptacle and holding in the same time a note. Swap notes.
   public void SwapNotes(NoteObject note)
    {
        NoteReceptacle tmp = Receptacle;
        Receptacle.SwapNote(note);
        Grab(null);
        tmp.gameObject.layer = LayerMask.NameToLayer("GvrCannotGrab");
    }
}
