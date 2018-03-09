using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonNote : Photon.MonoBehaviour
{
    private Vector3 correctNotePos;

    public Vector3 CorrectNotePos { get; set; }

    void Start()
    {
        CorrectNotePos = new Vector3(1, 1, 1);
    }

    void Update()
    {
       // Lerping smooths the movement
       if(!photonView.isMine)
            transform.position = Vector3.Lerp(transform.position, CorrectNotePos, Time.deltaTime * 5);   
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
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

    public void TransferOwnership()
    {
        photonView.TransferOwnership(PhotonNetwork.player.ID);
    }
}
