using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabber : AGrabber {

    private AGrabable collidingGrabable;
    private bool createdRigidbody = false;

    #region SteamVR Stuff
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public override Vector3 Forward
    {
        get
        {
            return trackedObj.transform.forward;
        }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    #endregion


    private void SetCollidingGrabable(Collider col)
    {
        if (!collidingGrabable && col.GetComponent<AGrabable>())
            collidingGrabable = col.GetComponent<AGrabable>();
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
        if (collidingGrabable && other.GetComponent<AGrabable>() == collidingGrabable)
            collidingGrabable = null;
    }

    private void GrabObject()
    {
        Joint joint;
        joint = AddFixedJoint();
        GrabbedObject = collidingGrabable.GetComponent<AGrabable>();
        collidingGrabable = null;
        IsGrabbing = true;
        GrabbedObject.Grab(this);
        createdRigidbody = !(GrabbedObject.GetComponent<Rigidbody>());
        if (createdRigidbody)
            GrabbedObject.gameObject.AddComponent<Rigidbody>();
        joint.connectedBody = GrabbedObject.GetComponent<Rigidbody>();
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

        if (joint && createdRigidbody)
            Destroy(joint.connectedBody);

        Destroy(joint);
        GrabbedObject.Release(this);
        IsGrabbing = false;
        GrabbedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.GetHairTriggerDown())
            if (collidingGrabable)
                GrabObject();

        if (Controller.GetHairTriggerUp())
            if (GrabbedObject)
                ReleaseObject();
    }
}
