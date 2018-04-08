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

    protected override void OnGrabbed()
    {
        base.OnGrabbed();
        var photonNote = GetComponent<PhotonNoteSynchro>();
        if (photonNote)
            photonNote.TransferOwnership();
    }

    protected override void RemoveNote()
    {
        NoteObject note = GvrPointerInputModule.Pointer.PointerTransform.GetComponentInChildren<NoteObject>();
        note.transform.SetParent(null);
        PhotonNetwork.Destroy(note.GetComponent<PhotonView>());
    }
}
