using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public SequencerNoteSource[] noteSources;
    public int[] partition;

    private Dictionary<SequencerNoteReceptacle, SequencerNoteSpawner> spawners;
    // Use this for initialization
    void Start () {
#if UNITY_ANDROID
        target = targetGoogleVr;
#else
        target = targetHtc;
#endif

     spawners = new Dictionary<SequencerNoteReceptacle, SequencerNoteSpawner>();
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
                float random = UnityEngine.Random.Range(0, 360);
                var vel = Quaternion.AngleAxis(random,Vector3.up) * Vector3.forward * 3f;
                rb.velocity = vel;
            }
        }

        foreach(var toggleStep in sequencer.StepsVisu)
        {
            foreach(var toggle in toggleStep.Toggles)
            {
                if (toggle.State)
                    toggle.Toggle();
            }
        }
    }

    public void Demo()
    {
        if (sequencer == null) return;

        Clear();

        for (int i = 0; i < sequencer.StepSize && i < noteSources.Length; i++)
        {
            var exSpawner = noteSources[i].spawner;
            noteSources[i].spawner = GetSpawner(sequencer.Receptacles[i]);
            noteSources[i].Play();
            noteSources[i].spawner = exSpawner;
        }

        for (int i = 0; i < partition.Length && i < sequencer.Steps; i++)
        {
            var step = sequencer.StepsVisu[i];
            var boolArray = partition[i].ToBooleanArray(step.Toggles.Length).Reverse().ToArray();
            for (int j = 0; j < step.Toggles.Length; j++)
                if (boolArray[j])
                    step.Toggles[j].Toggle();
        }
    }

    private SequencerNoteSpawner GetSpawner(SequencerNoteReceptacle receptacle)
    {
        SequencerNoteSpawner ret;
        if(!spawners.TryGetValue(receptacle, out ret))
        {
            ret = new MySpawner(receptacle);
            spawners.Add(receptacle, ret);
        }

        return ret;
    }

    private class MySpawner : SequencerNoteSpawner
    {
        private SequencerNoteReceptacle receptacle;
        private NoteObject noteObject;

        public MySpawner(SequencerNoteReceptacle receptacle)
        {
            this.receptacle = receptacle;
        }

        public override void SpawnNote(NoteObject noteObject, NoteSound note)
        {
            noteObject.transform.position = receptacle.transform.position;
            noteObject.note = note;
        }
    }
}

public static class Int32Extensions
{
    public static bool[] ToBooleanArray(this int i, int size)
    {
        var ret = Convert.ToString(i, 2 /*for binary*/).Select(s => s.Equals('1')).ToArray();
        int fillSize = size - ret.Length;
        for(int j = 0; j<fillSize; j++)
        {
            var z = new bool[ret.Length + 1];
            ret.CopyTo(z, 1);
            ret = z;
        }
        
        return ret;
    }
}
