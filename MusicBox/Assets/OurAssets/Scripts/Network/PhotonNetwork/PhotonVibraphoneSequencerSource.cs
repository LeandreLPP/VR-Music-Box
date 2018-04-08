using UnityEngine;

public class PhotonVibraphoneSequencerSource : VibraphoneSequencerSource
{

    protected override NoteObject InstantiateNoteObject()
    {

        var noteObject = PhotonNetwork.Instantiate(noteObjectPrefab.name, Vector3.zero, Quaternion.identity, 0, null).GetComponent<NoteObject>();
        var renderer = noteObject.gameObject.GetComponent<Renderer>();

        renderer.material.color = colorNote;
        return noteObject;
    }

    public override void Play()
    {
        Note note = new Note { audioClip = source.clip, volume = source.volume };
        NoteObject noteObject = InstantiateNoteObject();
        spawner.HandleNewNote(noteObject, note);
        noteObject.GetComponent<PhotonView>().RPC("UpdateNote", PhotonTargets.Others, note.audioClip.name, note.volume, colorNote.r, colorNote.g, colorNote.b); 
        source.Play();
    }
}