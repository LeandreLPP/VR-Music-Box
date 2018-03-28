using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.PunBehaviour {

    private Vector3 correctNotePos;
    private Transform Camera;

    public Vector3 CorrectNotePos { get; set; }
    public Quaternion CorrectNoteRot { get; set; }


    protected void Start()
    {
        if (photonView.isMine)
        {
            Camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
            transform.SetParent(Camera);
            transform.rotation = Camera.rotation;
        }
    }

    protected void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, CorrectNotePos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, CorrectNoteRot, Time.deltaTime * 5);
        }

    }

    protected void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            CorrectNotePos = (Vector3)stream.ReceiveNext();
            CorrectNoteRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
