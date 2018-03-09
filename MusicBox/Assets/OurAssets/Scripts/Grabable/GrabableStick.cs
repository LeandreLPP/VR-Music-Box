using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableStick : BaseGrabable {

    protected override void OnGrabbed()
    {
        base.OnGrabbed();
        GetComponent<Collider>().enabled = false;
    }

    protected override void OnRelease(AGrabber grabber)
    {
        base.OnRelease(grabber);
        GetComponent<Collider>().enabled = true;
    }
}
