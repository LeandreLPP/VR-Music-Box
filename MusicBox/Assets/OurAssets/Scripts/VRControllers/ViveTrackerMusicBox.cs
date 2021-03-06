﻿using UnityEngine;
using UnityEngine.Audio;
using Valve.VR;

public class ViveTrackerMusicBox : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private AudioSource music;

    public AudioMixer mixer;
    public string pitchParameterName;
    public SteamVR_PlayArea playArea;
    public TextMesh text;

    // Just for debugging purpose, don't actually need them to be public
    private Vector3[] corners;
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    // Use this for initialization
    void Start()
    {
        var rect = new HmdQuad_t();
        if (!SteamVR_PlayArea.GetBounds(SteamVR_PlayArea.Size.Calibrated, ref rect))
            return;

        // Could do without that
        var cornersVR = new HmdVector3_t[] { rect.vCorners0, rect.vCorners1, rect.vCorners2, rect.vCorners3 };

        corners = new Vector3[cornersVR.Length];
        for (int i = 0; i < cornersVR.Length; i++)
        {
            var c = cornersVR[i];
            corners[i] = new Vector3(c.v0, 0.01f, c.v2);
        }

        // Make sure this is always the case
        minX = -Mathf.Abs(corners[0].x);
        maxX = Mathf.Abs(corners[0].x);
        minZ = -Mathf.Abs(corners[0].z);
        maxZ = Mathf.Abs(corners[0].z);

        music = transform.GetComponent<AudioSource>();
        mixer = music.outputAudioMixerGroup.audioMixer;

        // Debugging
        text = GetComponentInChildren<TextMesh>();
    }

    private bool pointed;
    public bool Pointed
    {
        get
        {
            return pointed;
        }
        set
        {
            foreach (Transform child in transform)
                if (child.gameObject.tag == "Indicator")
                    child.gameObject.SetActive(value);
            pointed = value;
        }
    }

    public void ChangeSound(AudioClip clip)
    {
        music.clip = clip;
        music.Play();
    }

    public float RatioX
    {
        get
        {
            float ratioX = this.transform.localPosition.x;
            ratioX -= minX;
            ratioX /= (maxX - minX);
            return ratioX;
        }
    }

    public float RatioZ
    {
        get
        {
            float ratioZ = this.transform.localPosition.z;
            ratioZ -= minZ;
            ratioZ /= (maxZ - minZ);
            return ratioZ;
        }
    }

    // Debugging
    private float pitch;
    private float tempo;
    private float correctedPitch;

    private float ScaledRatio(float ratio, float min, float max, float step)
    {
        float ret = ratio * (max - min);
        ret += min;
        ret = Mathf.Round(ret / step) * step;
        return ret;
    }

    // Update is called once per frame
    void Update () {
        // Pitch and tempo are codependant
        pitch = ScaledRatio(RatioZ, -1f, 2f, 0.1f);
        tempo = ScaledRatio(RatioX, 0.5f, 2f, 0.1f);

        music.pitch = tempo;
        correctedPitch = pitch / tempo;

        mixer.SetFloat(pitchParameterName,Mathf.Max(-1.5f, Mathf.Min(2f, correctedPitch)));

        // Debug
        text.text = "Pitch:" + pitch + "\nTempo:" + tempo + "\nCorrected pitch:" + correctedPitch;
    }
}
