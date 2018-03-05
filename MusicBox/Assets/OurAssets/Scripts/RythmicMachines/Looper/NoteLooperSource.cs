using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class NoteSource : MonoBehaviour  {

    public AnimatedLooper sequencer;
    public LooperNoteObject noteObjectPrefab;

    private AudioSource source;

    protected virtual void Start()
    {
        source = GetComponent<AudioSource>();
    }

    protected void Play()
    {
        NoteSound note = new NoteSound { audioClip = source.clip, volume = source.volume };
        LooperNoteObject noteObject = Instantiate<GameObject>(noteObjectPrefab.gameObject, transform.position, transform.rotation).GetComponent<LooperNoteObject>();
        noteObject.note = note;
        noteObject.looper = sequencer;
        source.Play();
        if (!sequencer.AddNoteObject(sequencer.CurrentStep, noteObject))
            noteObject.GetComponent<Rigidbody>().useGravity = true;
    }

}
