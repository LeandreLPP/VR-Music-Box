using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawner : NetworkBehaviour {
    
    public GameObject steamVR;
    public GameObject cameraRig;
    public Camera lobbyCamera;
    public GameObject grabber;
        
    SteamVR_ControllerManager controllerManager;


    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("On Start Server");
        controllerManager = steamVR.GetComponent<SteamVR_ControllerManager>();
        GameObject[] toActivate = GameObject.FindGameObjectsWithTag("ServerSideOnly");
        foreach (var o in toActivate)
            o.GetComponent<MeshRenderer>().enabled = true;
        steamVR.SetActive(true);
        cameraRig.SetActive(true);
        lobbyCamera.gameObject.SetActive(false);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        GameObject[] toActivate = GameObject.FindGameObjectsWithTag("ClientSideOnly");
        foreach (var o in toActivate)
            o.GetComponent<MeshRenderer>().enabled = true;
        lobbyCamera.gameObject.SetActive(false);
        grabber.SetActive(true);
    }

    public override void OnNetworkDestroy()
    {
        lobbyCamera.gameObject.SetActive(true);
    }
}
