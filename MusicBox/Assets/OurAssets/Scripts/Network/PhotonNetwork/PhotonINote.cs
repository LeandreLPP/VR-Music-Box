﻿using UnityEngine;

public class PhotonINote : PhotonNoteSynchro
{

    private PhotonInstrumentPlayer spawner;

    private void Awake()
    {
        spawner = GameObject.Find("PhotonSoundSelector").GetComponent<PhotonInstrumentPlayer>();
    }

    //Call on each client when someone create a note to add this note to the spawner
    [PunRPC]
    public void SetSpawnerHeldNote()
    {
        spawner.NoteHeld = GetComponent<NoteObject>();
    }

    //Call on each client when someone grab the note held by the spawner to reset noteHeld to null.
    [PunRPC]
    public void RemoveSpawnerHeldNote()
    {
        spawner.NoteHeld = null;
    }
}
