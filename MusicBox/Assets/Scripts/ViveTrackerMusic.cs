using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Valve.VR;

public class ViveTrackerMusic : MonoBehaviour {

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
    public Vector3[] corners;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

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

    public float RatioX
    {
        get
        {
            float ratioX = this.transform.position.x;
            ratioX -= minX;
            ratioX /= (maxX - minX);
            return ratioX;
        }
    }

    public float RatioZ
    {
        get
        {
            float ratioZ = this.transform.position.z;
            ratioZ -= minZ;
            ratioZ /= (maxZ - minZ);
            return ratioZ;
        }
    }

    // Update is called once per frame
    void Update () {
        //music.volume = Mathf.Max(0.1f,Mathf.Min(1f, RatioX));

        // Pitch and tempo are codependant
        float pitch = Mathf.Round(((RatioZ * 2f) - 1.5f) * 10f) / 10f;

        float tempo = Mathf.Round(((RatioX * 3f) - 1f) * 10f) / 10f;

        music.pitch = tempo;

        float correctedPitch = pitch / tempo;

        mixer.SetFloat(pitchParameterName,Mathf.Max(-1.5f, Mathf.Min(2f, correctedPitch)));

        text.text = "Pitch:" + pitch + "\nTempo:" + tempo + "\nCorrected pitch:" + correctedPitch;
    }
}
