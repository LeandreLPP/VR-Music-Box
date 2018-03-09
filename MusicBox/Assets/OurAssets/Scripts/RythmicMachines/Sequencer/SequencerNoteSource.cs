using UnityEngine;
using System.Collections;

public class SequencerNoteSource : MonoBehaviour
{
    public NoteObject noteObjectPrefab;
    public SequencerNoteSpawner spawner;

    private AudioSource source;

    protected virtual void Start()
    {
        source = GetComponent<AudioSource>();
    }


    protected virtual NoteObject InstantiateNoteObject()
    {
        return Instantiate<GameObject>(noteObjectPrefab.gameObject).GetComponent<NoteObject>();
    }

    public void Play()
    {
        NoteSound note = new NoteSound { audioClip = source.clip, volume = source.volume };
        NoteObject noteObject = InstantiateNoteObject();
        spawner.SpawnNote(noteObject, note);

        source.Play();
    }

}
