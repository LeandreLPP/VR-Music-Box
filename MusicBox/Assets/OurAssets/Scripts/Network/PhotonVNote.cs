using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhotonVNote : PhotonNote {

    private PhotonSpawner spawner;

    private void Awake()
    {
            spawner = GameObject.FindGameObjectWithTag("PhotonSpawnerVibraphone").GetComponent<PhotonSpawner>();
    }

    [PunRPC]
    public void AddToSpwaner()
    {
        spawner.AddRPCNote(GetComponent<NoteObject>());
    }

    [PunRPC]
    public void RemoveToSpwaner(int index)
    {
        spawner.NotesHold.RemoveAt(index);
    }


}
