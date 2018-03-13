using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoogleVibraphone : VibraphoneSequencerSource
{

    public void OnClick (BaseEventData data) {
        Play();
    }

    protected override NoteObject InstantiateNoteObject()
    {

        var noteObject = PhotonNetwork.Instantiate(noteObjectPrefab.name, Vector3.zero, Quaternion.identity, 0, null).GetComponent<NoteObject>();
        var renderer = noteObject.gameObject.GetComponent<Renderer>();

        renderer.material.color = colorNote;
        return noteObject;
    }
}
