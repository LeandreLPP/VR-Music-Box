using UnityEngine;
using UnityEngine.EventSystems;

public class PhotonNote : GNote
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
                photonView.RPC("UpdateIsGrabbed", PhotonTargets.Others);

        }
    }

    public override bool TryGrab(IGrabber grab)
    {
        if (!CanGrab(grab))
            return false;

        var photonNote = GetComponent<PhotonNoteSynchro>();
        if (photonNote)
            photonNote.TransferOwnership();

        IsGrabbed = true;
        Grabber = grab;
        OnGrabbed();
        return true;
    }



    protected override void RemoveNote()
    {
        NoteObject note = GvrPointerInputModule.Pointer.PointerTransform.GetComponentInChildren<NoteObject>();
        note.transform.SetParent(null);
        PhotonNetwork.Destroy(note.GetComponent<PhotonView>());
    }
}
