using UnityEngine;
using UnityEngine.Networking;

public class FollowObject : NetworkBehaviour {

    public GameObject target;
    public Vector3 offsetPosition;
    public Vector3 offsetRotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isServer) return;

        transform.position = target.transform.position + offsetPosition;
        transform.eulerAngles = target.transform.eulerAngles + offsetRotation;
	}
}
