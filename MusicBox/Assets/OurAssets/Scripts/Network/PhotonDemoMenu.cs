using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonDemoMenu : AGrabber {

    public VisualSequencer sequencer;

    public GameObject targetHtc;
    public GameObject targetGoogleVr;

    protected GameObject target;

    public override Vector3 Forward
    {
        get
        {
            return transform.forward;
        }
    }

    public override Vector3 AngularVelocity
    {
        get {
            return Vector3.zero;
        }
    }

    public override Vector3 Velocity
    {
        get
        {
            return Vector3.zero;
        }
    }

    // Use this for initialization
    void Start () {
#if UNITY_ANDROID
        target = targetGoogleVr;
#else
        target = targetHtc;
#endif
    }

    // Update is called once per frame
    void Update () {
        // Turn toward target
        if (target != null)
            transform.LookAt(target.transform.position);
    }

    public void Clear()
    {
        if (sequencer == null) return;

        foreach(var receptacle in sequencer.Receptacles)
        {
            var noteObject = receptacle.NoteHold;
            if(noteObject)
            {
                var rb = noteObject.GetComponent<Rigidbody>();
                rb.useGravity = true;
                float random = Random.Range(0, 360);
                var vel = Quaternion.AngleAxis(random,Vector3.up) * Vector3.forward * 3f;
                rb.velocity = vel;
            }
        }

        foreach(var toggleStep in sequencer.StepsVisu)
        {
            foreach(var toggle in toggleStep.Toggles)
            {
                if (toggle.State)
                    (toggle as PhotonToggle).ClickOn(null);
            }
        }
    }

    public void Demo()
    {
        if (sequencer == null) return;

        Clear();

    }
}
