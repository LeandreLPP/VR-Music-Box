using UnityEngine;

public class Note
{
    public AudioClip audioClip;
    public float volume = 1;

    public Note Clone()
    {
        return new Note { audioClip = this.audioClip, volume = this.volume };
    }
}