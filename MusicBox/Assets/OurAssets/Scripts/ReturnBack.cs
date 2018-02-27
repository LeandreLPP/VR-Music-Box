using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBack : Grabable {

    // Use this for initialization

    Vector3 originalPos;
    Quaternion originalRot;

    float minDist = 0.2f;
    float maxDist = 2f;

    void Awake ()
    {
        Grabbed = false;
        originalRot = transform.rotation;
        originalPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (Grabbed)
            return;

        if (!originalPos.Equals(transform.position))
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime);

        if (!originalRot.Equals(transform.localRotation))
            transform.localRotation = Quaternion.Lerp(transform.localRotation, originalRot, Time.deltaTime);
    }
}
