using UnityEngine;

public class PhotonInstrumentPlayer : UISoundChooser
{


    public override void HandleNewNote(NoteObject noteObject, Note note)
    {
        noteObject.note = note;
        noteObject.transform.SetParent(spawnPoint.transform);
        noteObject.transform.localPosition = Vector3.zero;
        if (NoteHeld != null)
        {
            PhotonView photonView = NoteHeld.GetComponent<PhotonView>();
            if (!photonView.isMine)
                NoteHeld.GetComponent<PhotonNoteSynchro>().TransferOwnership();
            PhotonNetwork.Destroy(NoteHeld.gameObject);
        }

        NoteHeld = noteObject;
        noteObject.GetComponent<PhotonView>().RPC("SetSpawnerHeldNote", PhotonTargets.Others);
    }

    protected override void Update()
    {
        var pos = transform.position + transform.right.normalized * Index * distance;
        rack.transform.position = Vector3.MoveTowards(rack.transform.position, pos, distance * 2f * Time.deltaTime);
        rack.transform.rotation = transform.rotation;

        if (rack.transform.localPosition == pos)
        {
            for (int i = 0; i < instruments.Length; i++)
                if (Index != i)
                    instruments[i].SetActive(false);
        }

        // Turn toward target
        if (target != null)
            transform.LookAt(target.transform.position);


        // Remove NoteHeld once grabbed
        if (NoteHeld != null && NoteHeld.IsGrabbed)
        {
            NoteHeld.GetComponent<PhotonView>().RPC("RemoveSpawnerHeldNote", PhotonTargets.Others);
            NoteHeld = null;
        }
    }
}
