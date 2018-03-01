using UnityEngine;

public class NoteSound
{
    public AudioClip audioClip;
    public float volume = 1;

    public NoteSound Clone()
    {
        return new NoteSound { audioClip = this.audioClip, volume = this.volume };
    }
}