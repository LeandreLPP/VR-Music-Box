using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeVelocity : MonoBehaviour {

    protected Vector3 lastPos;

    public Vector3 velocity
    {
        get; set;
    }
    
	void Start ()
    {
        lastPos = transform.position;
    }
	
	void FixedUpdate ()
    {
        velocity = transform.position - lastPos;
        lastPos = transform.position;
	}
}
