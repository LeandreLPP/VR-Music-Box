using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.PunBehaviour {

    private Vector3 correctNotePos;
    private Transform Camera;

    public Vector3 CorrectNotePos { get; set; }


    protected void Start()
    {
        CorrectNotePos = new Vector3(0, 0, 0);
        Camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        if (!photonView.isMine)
        {
            transform.SetParent(Camera);
            transform.rotation = Camera.rotation;
        }
    }

    protected void Update()
    {
        if (!photonView.isMine)
            transform.position = Vector3.Lerp(transform.position, CorrectNotePos, Time.deltaTime * 5);
    }

    protected void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            CorrectNotePos = (Vector3)stream.ReceiveNext();
        }
    }
}
