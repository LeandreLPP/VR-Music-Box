using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabSound : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;

    public GameObject laserPrefab;
        public GameObject stickPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;

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
        if (!collidingObject && col.GetComponent<Rigidbody>() && col.GetComponent<Grabable>())
            collidingObject = col.gameObject;
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
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private bool SoundInHand
    {
        get
        {
            return objectInHand && objectInHand.GetComponent<SoundLoop>();
        }
    }
    private void GrabObject()
    {
        if (collidingObject.tag.Equals("Stick"))
        {
            GameObject stick = Instantiate(stickPrefab);

            stick.transform.position = collidingObject.transform.position;
            stick.transform.rotation = collidingObject.transform.rotation;

            objectInHand = stick;
        }
        else
        {
            objectInHand = collidingObject;
        }
        collidingObject = null;
        objectInHand.GetComponent<Collider>().enabled = false;
        objectInHand.GetComponent<Grabable>().Grabbed = true;

        Joint joint;
        joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private Joint AddHingeJoint()
    {
        HingeJoint fx = gameObject.AddComponent<HingeJoint>();

        fx.useSpring = true;
        JointSpring sp = new JointSpring
        {
            spring = 500,
            damper = 0.5f
        };
        fx.spring = sp;
        fx.limits = new JointLimits
        {
            max = 50
        };

        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
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
        else if (GetComponent<HingeJoint>())
            joint = GetComponent<HingeJoint>();

        Debug.Log(objectInHand.tag);

        if (objectInHand.tag.Equals("Stick"))
        {
            joint.connectedBody = null;
            Destroy(joint);
            Destroy(objectInHand);
        }
        else
        {
            if (joint != null)
            {
                objectInHand.GetComponent<Collider>().enabled = true;

                joint.connectedBody = null;
                Destroy(joint);

                if (SoundInHand && musicBox)
                    musicBox.ChangeSound(objectInHand.GetComponent<SoundLoop>());
            }
            objectInHand.GetComponent<Grabable>().Grabbed = false;
            objectInHand = null;
        }
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
        
        if (SoundInHand)
        {
            RaycastHit hit;

            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100))
            {
                hitPoint = hit.point;
                ShowLaser(hit);

                GameObject objectHit = hit.collider.gameObject;
                if (objectHit.GetComponent<ViveTrackerMusic>())
                {
                    if (musicBox != objectHit.GetComponent<ViveTrackerMusic>())
                        objectHit.GetComponent<ViveTrackerMusic>().Pointed = false;

                    musicBox = objectHit.GetComponent<ViveTrackerMusic>();
                    musicBox.Pointed = true;
                }
                else
                {
                    if (musicBox)
                    {
                        musicBox.Pointed = false;
                        musicBox = null;
                    }
                }
            }
            else
            {
                if(musicBox)
                {
                    musicBox.Pointed = false;
                    musicBox = null;
                }
                laser.SetActive(false);
            }
        }
        else
        {
            if (musicBox)
            {
                musicBox.Pointed = false;
                musicBox = null;
            }
            laser.SetActive(false);
        }
    }
}
