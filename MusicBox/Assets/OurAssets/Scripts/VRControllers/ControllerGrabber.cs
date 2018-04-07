using UnityEngine;

public class ControllerGrabber : MonoBehaviour, IGrabber {

    private IGrabable collidingGrabable;
    private bool createdRigidbody = false;

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


    private void SetCollidingGrabable(Collider col)
    {
        if (collidingGrabable == null && col.GetComponent<IGrabable>() != null)
        {
            collidingGrabable = col.GetComponent<IGrabable>();
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
            collidingGrabable = null;
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

#region Grabber implementation
    public Vector3 Forward
    {
        get
        {
            return trackedObj.transform.forward;
        }
    }

    public Vector3 AngularVelocity
    {
        get
        {
            return Controller.angularVelocity;
        }
    }

    public Vector3 Velocity
    {
        get
        {
            return Controller.velocity;
        }
    }

    public bool IsGrabbing
    {
        get { return GrabbedObject != null; }
    }

    public IGrabable GrabbedObject { get; protected set; }

    private void GrabObject()
    {
        var grabable = collidingGrabable;
        if(grabable != null && grabable.TryGrab(this))
        {
            GrabbedObject = grabable;

            Joint joint;
            joint = AddFixedJoint();

            collidingGrabable = null;

            createdRigidbody = !((GrabbedObject as MonoBehaviour).GetComponent<Rigidbody>());
            if (createdRigidbody)
                (GrabbedObject as MonoBehaviour).gameObject.AddComponent<Rigidbody>();
            joint.connectedBody = (GrabbedObject as MonoBehaviour).GetComponent<Rigidbody>();
        }
    }

    private void ReleaseObject()
    {
        GrabbedObject.TryRelease(this);
        GrabbedObject = null;
        Joint joint = null;

        if (GetComponent<FixedJoint>())
            joint = GetComponent<FixedJoint>();

        if (joint && createdRigidbody)
            Destroy(joint.connectedBody);

        Destroy(joint);
    }
#endregion

    // Update is called once per frame
    void Update()
    {

        if (Controller.GetHairTriggerDown())
        {
            if (collidingGrabable != null && !collidingGrabable.IsGrabbed)
                GrabObject();
        }
                       
        if (Controller.GetHairTriggerUp())
            if (GrabbedObject != null)
                ReleaseObject();
    }
}
