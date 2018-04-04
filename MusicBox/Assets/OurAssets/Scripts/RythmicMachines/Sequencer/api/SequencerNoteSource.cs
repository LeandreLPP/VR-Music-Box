using UnityEngine;

public class SequencerNoteSource : MonoBehaviour
{
    public NoteObject noteObjectPrefab;
    public ISequencerNoteHandler spawner;
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
        Note note = new Note { audioClip = source.clip, volume = source.volume };
        NoteObject noteObject = InstantiateNoteObject();
        spawner.HandleNewNote(noteObject, note);

        source.Play();
    }

}
