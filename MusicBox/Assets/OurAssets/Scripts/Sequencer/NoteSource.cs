using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NoteSource : MonoBehaviour  {

    public AnimatedSequencer sequencer;

    public NoteObject noteObjectPrefab;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play()
    {
        NoteSound note = new NoteSound { audioClip = source.clip, volume = source.volume };
        NoteObject noteObject = Instantiate<GameObject>(noteObjectPrefab.gameObject, transform.position, transform.rotation).GetComponent<NoteObject>();
        noteObject.note = note;
        noteObject.sequencer = sequencer;
        source.Play();
        if (!sequencer.AddNoteObject(sequencer.CurrentStep, noteObject))
            noteObject.GetComponent<Rigidbody>().useGravity = true;
    }

}
