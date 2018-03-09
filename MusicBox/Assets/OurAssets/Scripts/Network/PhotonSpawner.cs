using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonSpawner : StackingSpawner
{


    public override void SpawnNote(NoteObject noteObjectPrefab, NoteSound note)
    {
        NoteObject noteObject = PhotonNetwork.InstantiateSceneObject(noteObjectPrefab.name, PositionNote(notesHold.Count), transform.rotation,0,null).GetComponent<NoteObject>();
        noteObject.transform.SetParent(rail.transform);
        noteObject.note = note;
        notesHold.Add(noteObject);
    }
}
