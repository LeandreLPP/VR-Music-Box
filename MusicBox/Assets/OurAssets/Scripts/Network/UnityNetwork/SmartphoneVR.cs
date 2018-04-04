using UnityEngine;
using UnityEngine.Networking;

public class SmartphoneVR : NetworkBehaviour {

    public override void OnStartClient()
    {
        base.OnStartClient();

        foreach (var c in GetComponentsInChildren<Camera>())
            c.enabled = true;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        GameObject[] trackers = GameObject.FindGameObjectsWithTag("SmartphoneTracker");
        bool attached = false;
        foreach(var t in trackers)
        {
            if(!t.GetComponentInChildren<SmartphoneVR>())
            {
                transform.SetParent(t.transform);
                transform.localPosition = Vector3.zero;
                transform.localRotation = new Quaternion();
                attached = true;
                break;
            }
        }

        if (!attached)
        {
            GameObject t = GameObject.FindGameObjectWithTag("MainCamera");

            transform.SetParent(t.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = new Quaternion();
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
