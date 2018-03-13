using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerTempoScript : MonoBehaviour {

    public Sequencer sequencer;
    public GameObject scalePointer;

    public int min = 10;
    public int max = 200;
    public float speed = 10f;

    private int middle;
    
    #region SteamVR Stuff
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    #endregion

    // Use this for initialization
    void Start () {
        middle = (max - min) / 2;
	}

    public float angularVelZ;
	// Update is called once per frame
	void Update () {
        var rot = scalePointer.transform.localEulerAngles;
        rot.y = -transform.eulerAngles.z;
        scalePointer.transform.localEulerAngles = rot;
        
        if (sequencer != null)
        {
            angularVelZ += Controller.angularVelocity.z * speed;
            int result = Mathf.FloorToInt(middle + angularVelZ);
            sequencer.tempo = Mathf.Max(min, Mathf.Min(max, result));
        }
	}
}
