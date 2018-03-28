using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.PunBehaviour {

    private Vector3 correctNotePos;
    private Transform Camera;

    public Vector3 CorrectNotePos { get; set; }


    protected void Start()
    {
        if (photonView.isMine)
        {
            Camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
            transform.SetParent(Camera,false);
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
