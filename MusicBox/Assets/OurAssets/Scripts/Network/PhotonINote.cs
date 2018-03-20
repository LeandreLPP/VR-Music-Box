using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonINote : PhotonNote {

    private PhotonInstrumentPlayer spawner;

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("PhotonCanvas").GetComponent<PhotonInstrumentPlayer>();
    }

    [PunRPC]
    public void SetSpawnerHeldNote()
    {
        spawner.NoteHeld = GetComponent<NoteObject>();
    }
}
