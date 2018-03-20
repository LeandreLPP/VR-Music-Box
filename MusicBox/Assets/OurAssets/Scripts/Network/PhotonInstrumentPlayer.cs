using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonInstrumentPlayer : InstrumentPlayer {


    public override void SpawnNote(NoteObject noteObject, NoteSound note)
    {
        noteObject.note = note;
        noteObject.transform.SetParent(spawnPoint.transform);
        noteObject.transform.localPosition = Vector3.zero;
        if (NoteHeld != null)
        {
            PhotonView photonView = NoteHeld.GetComponent<PhotonView>();
            if (!photonView.isMine)
                NoteHeld.GetComponent<PhotonNote>().TransferOwnership();
            PhotonNetwork.Destroy(NoteHeld.gameObject);
        }

        NoteHeld = noteObject;
        noteObject.GetComponent<PhotonView>().RPC("SetSpawnerHeldNote", PhotonTargets.OthersBuffered);
    }
}
