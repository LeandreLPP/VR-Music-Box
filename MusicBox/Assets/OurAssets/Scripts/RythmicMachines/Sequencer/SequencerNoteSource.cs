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

    protected virtual void Play()
    {
        NoteSound note = new NoteSound { audioClip = source.clip, volume = source.volume };
        spawner.SpawnNote(noteObjectPrefab, note);

        source.Play();
    }

}
