using UnityEngine;
using System.Collections;
using UnityEngine.XR;
using UnityEngine.UI;

public class PhotonManager : Photon.PunBehaviour
{

    public GameObject GoogleVr;
    public GameObject CameraRig;
    public GameObject SteamVr;
    private GameObject[] canvas;

    public GameObject playerPrefab;
    private GameObject player;

    // Use this for initialization
    void Start () {
        PhotonNetwork.ConnectUsingSettings("v.0.0.1");
        PhotonNetwork.autoCleanUpPlayerObjects = false;
        canvas = GameObject.FindGameObjectsWithTag("PhotonCanvas");
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
        foreach(GameObject go in canvas)
            go.GetComponent<GvrPointerGraphicRaycaster>().enabled = true;
        StartCoroutine(LoadDevice("Daydream"));
#else
        foreach (GameObject go in canvas)
            go.GetComponent<GraphicRaycaster>().enabled = true;
        if(!CameraRig.activeInHierarchy)
            CameraRig.SetActive(true);
        if(!SteamVr.activeInHierarchy)
            SteamVr.SetActive(true);
#endif

        Debug.Log("number of player in the room " + PhotonNetwork.countOfPlayers);

        player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
    }


    //Enable or disable VR 
    IEnumerator LoadDevice(string newDevice)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = true;
    }


    //When a play join the game, the masterClient will give him all the statements of the objects
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        
        if (PhotonNetwork.player.IsMasterClient)
        {

            GameObject sequencer = GameObject.Find("PhotonSequencer");
            //Update the switches
            var sequencerUI = sequencer.GetComponent<SequencerUI>().StepsVisu;
            for (int i = 0; i < sequencerUI.Length; i++)
            {
                for (int j = 0; j < sequencerUI[i].Toggles.Length; j++)
                {
                    if (sequencerUI[i].Toggles[j].State)
                        sequencer.GetComponent<PhotonView>().RPC("UpdateToggle", newPlayer, i, j, true);
                }
            }

            //Update the notes
            NoteObject[] notes = FindObjectsOfType<NoteObject>();
            foreach(NoteObject noteObject in notes)
            {
                //Update the sound and the color
                Color color = noteObject.gameObject.GetComponent<Renderer>().material.color;
                noteObject.GetComponent<PhotonView>().RPC("UpdateNote", newPlayer,noteObject.note.audioClip.name, noteObject.note.volume, color.r, color.g, color.b);

                //Update IsGrabbed
                if (noteObject.GetComponent<PhotonNote>().IsGrabbed)
                    noteObject.GetComponent<PhotonView>().RPC("UpdateIsGrabbed", newPlayer);

            }


            //Update the current tempo
            sequencer.GetComponent<PhotonView>().RPC("UpdateTempo", PhotonTargets.Others, sequencer.GetComponent<SequencerUI>().Sequencer.Tempo);

            //Update instrument spawner
            var instrumentSpawner = FindObjectOfType<PhotonInstrumentPlayer>();
            if(instrumentSpawner.NoteHeld != null)
                instrumentSpawner.NoteHeld.GetComponent<PhotonView>().RPC("SetSpawnerHeldNote", newPlayer);

            //Update vibraphone spawner
            var vibraphoneSpawner = FindObjectOfType<PhotonSpawner>();
            if(vibraphoneSpawner.NotesHold.Count != 0)
            {
                foreach (NoteObject noteObject in vibraphoneSpawner.NotesHold)
                {
                    noteObject.GetComponent<PhotonView>().RPC("AddToSpwaner", newPlayer);
                }
            }

        }
    }


}
