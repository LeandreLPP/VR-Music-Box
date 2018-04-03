using UnityEngine;

public class SequencerNoteSource : MonoBehaviour
{
    public NoteObject noteObjectPrefab;
    public SequencerNoteSpawner spawner;
    public Color colorNote;

    protected AudioSource source;

    protected virtual void Start()
    {
        source = GetComponent<AudioSource>();
    }

    protected virtual NoteObject InstantiateNoteObject()
    {
        var ret = Instantiate<GameObject>(noteObjectPrefab.gameObject).GetComponent<NoteObject>();
        ret.GetComponent<MeshRenderer>().material.color = colorNote;
        return ret;
    }

    public virtual void Play()
    {
        NoteSound note = new NoteSound { audioClip = source.clip, volume = source.volume };
        NoteObject noteObject = InstantiateNoteObject();
        spawner.SpawnNote(noteObject, note);

        source.Play();
    }

}
