using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GUIConnectServer : MonoBehaviour {

    public Text addressText;
    //public Text portText;
    public NetworkManager manager;
    public Button connectButton;

    private void Start()
    {
#if UNITY_ANDROID
#else
        manager.StartServer();
        gameObject.SetActive(false);
#endif
    }

    public void TryConnect()
    {
        manager.networkAddress = addressText.text;
        //manager.networkPort = Int32.Parse(portText.text);
        connectButton.interactable = false;
        manager.StartClient();
        gameObject.SetActive(false);
    }
    
}
