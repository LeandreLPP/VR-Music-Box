using UnityEngine;
using UnityEngine.Networking;

public class TrackerGrabber : MonoBehaviour, IGrabber
{
    public float delay = 1f;

    private IGrabable collidingGrabable;
    private IGrabable copiedGrabable;

    private float enteredTime;
    
    private void SetCollidingGrabable(Collider col)
    {
        var grabable = col.GetComponent<IGrabable>();
        if (collidingGrabable == null &&  grabable != null && grabable != GrabbedObject && col.tag == "Stick")
        {
            collidingGrabable = col.GetComponent<IGrabable>();
            enteredTime = Time.time;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingGrabable(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingGrabable(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (collidingGrabable != null && other.GetComponent<IGrabable>() == collidingGrabable)
        {
            collidingGrabable = null;
        }
    }

    #region Grabber implementation
    public Vector3 Forward
    {
        get
        {
            return transform.forward;
        }
    }

    public Vector3 AngularVelocity
    {
        get
        {
            return Vector3.zero;
        }
    }

    public Vector3 Velocity
    {
        get
        {
            return Vector3.zero;
        }
    }

    public bool IsGrabbing
    {
        get { return GrabbedObject != null; }
    }

    public IGrabable GrabbedObject { get; protected set; }

    private void GrabObject()
    {
        var go = (collidingGrabable as MonoBehaviour).gameObject;
        var copy = Instantiate(go, gameObject.transform.position, go.transform.rotation) as GameObject;
        var networkTransform = copy.GetComponent<NetworkTransform>();
        var networkID = copy.GetComponent<NetworkIdentity>();
        Destroy(networkTransform);
        Destroy(networkID);

        var grabable = copy.GetComponent<IGrabable>();
        if (grabable != null && grabable.TryGrab(this))
        {
            GrabbedObject = grabable;
            copiedGrabable = collidingGrabable;
            collidingGrabable = null;

            copy.transform.SetParent(transform);
        }
        else Destroy(copy);
    }

    private void ReleaseObject()
    {
        GrabbedObject.TryRelease(this);

        var tr = (GrabbedObject as MonoBehaviour).gameObject.transform;
        tr.SetParent(null);
        tr.position = new Vector3(0, -500, 0);
        Destroy((GrabbedObject as MonoBehaviour).gameObject);
        GrabbedObject = null;

    }
    #endregion

    private void Start()
    {
        enteredTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        Color color = new Color(1, 1, 1, 1);
        if(collidingGrabable != null)
        {
            float timeSpent = Time.time - enteredTime;
            float ratio = 1 - (timeSpent / delay);
            if(!IsGrabbing)
                color = new Color(ratio, 1, ratio, 1);
            else if (collidingGrabable == copiedGrabable)
                color = new Color(1, ratio, ratio, 1);

            if (timeSpent >= delay)
            {
                if (!IsGrabbing)
                    GrabObject();
                else if (collidingGrabable == copiedGrabable)
                    ReleaseObject();
                enteredTime = Time.time;
            }
        }
        var i = Shader.PropertyToID("_Color");
        var c = GetComponent<MeshRenderer>().material.GetColor(i);
        GetComponent<MeshRenderer>().material.SetColor(i, color);
    }
}
