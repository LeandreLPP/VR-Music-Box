using UnityEngine;

public class TrackerGrabObject : MonoBehaviour {

    /*
    // Use this for initialization
    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;
    public GameObject stickPrefab;


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start()
    {

    }


    private void SetCollidingObject(Collider col)
    {


    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Stick") && !objectInHand)
            GrabObject(other);
        else if (other.tag.Equals("Stick") && objectInHand)
            ReleaseObject();
    }


   
    private void GrabObject(Collider other)
    {
        GameObject stick = Instantiate(stickPrefab);
        collidingObject = other.gameObject;

        stick.transform.position = collidingObject.transform.position;
        stick.transform.rotation = collidingObject.transform.rotation;

        objectInHand = stick;
        collidingObject = null;
        objectInHand.GetComponent<Collider>().enabled = false;
        objectInHand.GetComponent<AGrabable>().Grabbed = true;
        Joint joint;
        joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

   

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        Joint joint = null;
        if (GetComponent<FixedJoint>())
            joint = GetComponent<FixedJoint>();

        if (joint != null)
        {
            joint.connectedBody = null;
            Destroy(joint);
            Destroy(objectInHand);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
       
}
