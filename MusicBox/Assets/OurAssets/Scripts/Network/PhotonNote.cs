﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonNote : Photon.PunBehaviour
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



    //When we create a note, we have to call this nethod on the others clients to update the audioclip and the material of the note, we have just created and we play its sound
    [PunRPC]
    public void UpdateNote(string clip, float volume, float r, float g, float b)
    {
        NoteSound note = GetComponent<GNote>().note;
        note = new NoteSound { audioClip = AudioClipDictionnary.audioClips[clip], volume = volume };
        GetComponent<MeshRenderer>().material.color = new Color(r, g, b);
        AudioSource source = GetComponent<AudioSource>(); 
        source.clip = note.audioClip;
        source.volume = note.volume;
        source.Play();
    }
}
