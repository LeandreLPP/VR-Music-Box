using UnityEngine;
using System.Collections;
using UnityEngine.XR;
using UnityEngine.UI;

public class PhotonManager : Photon.PunBehaviour
{

    public GameObject GoogleVr;
    public GameObject CameraRig;
    public GameObject SteamVr;
    private GameObject canvas;

    // Use this for initialization
    void Start () {
        PhotonNetwork.ConnectUsingSettings("v.0.0.1");
        PhotonNetwork.autoCleanUpPlayerObjects = false;
        canvas = GameObject.FindGameObjectWithTag("PhotonCanvas");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public override void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("MusicBox", roomOptions, TypedLobby.Default);

    }

    public override void OnJoinedRoom()
    {

#if UNITY_ANDROID
        GoogleVr.SetActive(true);
        canvas.GetComponent<GvrPointerGraphicRaycaster>().enabled = true;
        StartCoroutine(LoadDevice("Daydream"));
#else
        canvas.GetComponent<GraphicRaycaster>().enabled = true;
        if(!CameraRig.activeInHierarchy)
            CameraRig.SetActive(true);
        if(!SteamVr.activeInHierarchy)
            SteamVr.SetActive(true);
#endif

        Debug.Log("number of player in the room " + PhotonNetwork.countOfPlayers);
    }

    //Enable or disable VR 
    IEnumerator LoadDevice(string newDevice)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = true;
    }

}
