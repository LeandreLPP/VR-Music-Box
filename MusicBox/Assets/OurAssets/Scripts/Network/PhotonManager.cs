using UnityEngine;
using System.Collections;
using UnityEngine.XR;

public class PhotonManager : Photon.PunBehaviour
{

    public GameObject GoogleVr;
    public GameObject CameraRig;
    public GameObject SteamVr;

    // Use this for initialization
    void Start () {
        PhotonNetwork.ConnectUsingSettings("v.0.0.1");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("MusicBox", roomOptions, TypedLobby.Default);

        if (XRSettings.supportedDevices[1].Equals("daydream"))
        {
            GoogleVr.SetActive(true);
            StartCoroutine(LoadDevice("Daydream"));
        }
        else
        {
            CameraRig.SetActive(true);
            SteamVr.SetActive(true);
        }
    }

    //Enable or disable VR 
    IEnumerator LoadDevice(string newDevice)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = true;
    }
}
