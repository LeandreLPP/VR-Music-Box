using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonInstrumentPlayer : InstrumentPlayer {


    public override void SpawnNote(NoteObject noteObject, NoteSound note)
    {
        noteObject.note = note;
        noteObject.transform.SetParent(spawnPoint.transform);
        noteObject.transform.localPosition = Vector3.zero;
        if (noteHeld != null)
            PhotonNetwork.Destroy(noteHeld.gameObject);
        noteHeld = noteObject;
    }
}
