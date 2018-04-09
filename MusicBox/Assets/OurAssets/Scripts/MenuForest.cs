using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuForest : MonoBehaviour {
    
    public GameObject target;
    public Button buttonSwitch;
    
    void Start()
    {
#if UNITY_ANDROID
#else
        buttonSwitch.interactable = true;
#endif
    }
    
    void Update()
    {
        // Turn toward target
        if (target != null)
            transform.LookAt(target.transform.position);
    }

    public void Switch()
    {
        FindObjectOfType<NetworkManager>().StopServer();
        SceneManager.LoadScene("NetworkedSequencer");
    }
}
