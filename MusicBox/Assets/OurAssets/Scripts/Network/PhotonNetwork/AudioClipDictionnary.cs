﻿using System.Collections.Generic;
using UnityEngine;

public class AudioClipDictionnary : MonoBehaviour {

    public static Dictionary<string, AudioClip> audioClips;

    void Start()
    {
        audioClips = new Dictionary<string, AudioClip>();

        AudioClip[] clips = Resources.LoadAll<AudioClip>("AudioClips/");
        foreach (AudioClip clip in clips)
            audioClips.Add(clip.name, clip);

    }  
}
