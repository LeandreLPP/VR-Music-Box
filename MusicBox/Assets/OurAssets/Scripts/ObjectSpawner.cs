using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR;

public class ObjectSpawner : NetworkBehaviour {

    public GameObject GoogleVr;
    public GameObject CameraRig;
    public GameObject SteamVr;

    private void Start()
    {

    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("On Start Server");
        //StartCoroutine(LoadDevice("Daydream"));
        /*CameraRig.SetActive(true);
        SteamVr.SetActive(true);*/
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        //StartCoroutine(LoadDevice("Daydream"));
    }

    public override void OnNetworkDestroy()
    {

    }


    //Enable or disable VR 
    IEnumerator LoadDevice(string newDevice)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = true;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
