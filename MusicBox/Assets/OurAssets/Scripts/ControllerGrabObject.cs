using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;

    public GameObject laserPrefab;
    public GameObject stickPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;
    private GameObject collidingSquare;

    //public LayerMask musicBoxMask;
    private ViveTrackerMusic musicBox;

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

        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
    }

    private void ShowLaser(RaycastHit hit)
    {

        laser.SetActive(true);

        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);

        laserTransform.LookAt(hitPoint);

        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
    }

    private void SetCollidingObject(Collider col)
    {
        if (col.tag.Equals("Sound"))
            collidingObject = col.gameObject;
        else if (col.tag.Equals("Square"))
            collidingSquare = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (collidingObject)
            collidingObject = null;
        else if (collidingSquare)
            collidingSquare = null;
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        
        collidingObject = null;
        objectInHand.GetComponent<Collider>().enabled = false;
        objectInHand.GetComponent<Grabable>().Grabbed = true;

        Joint joint;
        joint = objectInHand.GetComponent<Joint>();
        if (joint)
            Destroy(joint);


        joint = AddFixedJoint(gameObject);
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint(GameObject gameObject)
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        //Destroy joint between the controller and the sound
        Joint joint = null;
        if (GetComponent<FixedJoint>())
            joint = GetComponent<FixedJoint>();
            
        if (joint != null)
        {
            objectInHand.GetComponent<Collider>().enabled = true;
            joint.connectedBody = null;
            Destroy(joint);
        }

        if (collidingSquare)
        {
            SetSound();
        }

        objectInHand.GetComponent<Grabable>().Grabbed = false;
        objectInHand = null;

    }

    void SetSound()
    {
        //Set a new joint between the sound and the square
        Joint joint = AddFixedJoint(objectInHand);
        joint.connectedBody = collidingSquare.GetComponent<Rigidbody>();
        collidingSquare.GetComponent<Collider>().enabled = false;
        collidingSquare = null;
    }


    // Update is called once per frame
    void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }
}
