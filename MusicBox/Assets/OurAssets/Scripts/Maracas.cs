using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Maracas : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private AudioSource music;
    private Vector3 oldPosition;
    private Vector3 oldDirection;

    public string pitchParameterName;

    public int nbFrameKept;
    private FixedSizedQueue<Vector3> lastDirections;
    public float threshold = 0.015f;

    public SteamVR_PlayArea playArea;


    // Use this for initialization
    void Start()
    {
        var rect = new HmdQuad_t();
        if (!SteamVR_PlayArea.GetBounds(SteamVR_PlayArea.Size.Calibrated, ref rect))
            return;


        music = GetComponent<AudioSource>();
        lastDirections = new FixedSizedQueue<Vector3>(nbFrameKept);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsShaking())
            music.Play();
    }

    private Vector3 MeanDirection
    {
        get
        {
            Vector3 ret = new Vector3(0, 0, 0);
            int n = 0;
            foreach (Vector3 v in lastDirections)
            {
                ret += v;
                n++;
            }
            ret /= n;
            return ret;
        }
    }
    
    bool IsShaking()
    {
        Vector3 direction = transform.position - oldPosition;

        Vector3 diffDirection = direction - MeanDirection;

        bool shaking = false;
       
        if (Vector3.Magnitude(diffDirection) > threshold)
        {
            shaking = true;
            lastDirections.Clear();
        }
        oldPosition = transform.position;
        lastDirections.Enqueue(direction);
        return shaking; 
        /*
        bool shaking = false;
        float cos = Vector3.Dot(direction, oldDirection) / (Vector3.Magnitude(direction) * Vector3.Magnitude(oldDirection));
        if (HasMoved())
        {
            //Debug.Log(cos);
            if (cos < 0)
            {
                shaking = true;
                Debug.Log("direction changed");
            }
        }

        oldPosition = transform.position;
        oldDirection = direction;
        return shaking;*/
    }

    bool HasMoved()
    {
        if ((transform.position - oldPosition).magnitude > 0.01)
        {
           // Debug.Log("HasMoved");
            return true;
        }

        return false;
    }

    public class FixedSizedQueue<T> : Queue<T>
    {
        Queue<T> q = new Queue<T>();

        public int Limit { get; set; }

        public FixedSizedQueue(int limit)
        {
            Limit = limit;
        }

        public new void Enqueue(T obj)
        {
            q.Enqueue(obj);
            while (q.Count > Limit) q.Dequeue();
        }
        public new void Clear()
        {
            q.Clear();
        }
        public new bool Contains(T item)
        {
            return q.Contains(item);
        }

        public new void CopyTo(T[] array, int idx)
        {
            q.CopyTo(array, idx);
        }
        public new T Dequeue()
        {
            return q.Dequeue();
        }
        public new Enumerator GetEnumerator()
        {
            return q.GetEnumerator();
        }
        public new T Peek()
        {
            return q.Peek();
        }
        public new T[] ToArray()
        {
            return q.ToArray();
        }
        public new void TrimExcess()
        {
            q.TrimExcess();
        }
    }
}
