using UnityEngine;

public class PhotonNoteSynchro : Photon.PunBehaviour
{
    private Vector3 correctNotePos;

    public Vector3 CorrectNotePos { get; set; }



    protected void Start()
    {
        CorrectNotePos = new Vector3(1, 1, 1);

    }

    protected void Update()
    {
        // Lerping smooths the movement
        if (!photonView.isMine)
            transform.position = Vector3.Lerp(transform.position, CorrectNotePos, Time.deltaTime * 5);
        if (photonView.isMine && transform.position.y < -10)
            PhotonNetwork.Destroy(this.gameObject);
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

    public void TransferOwnership()
    {
        photonView.TransferOwnership(PhotonNetwork.player.ID);
    }



    //When we create a note, we have to call this nethod on the others clients to update the audioclip and the material of the note, we have just created and we play its sound
    [PunRPC]
    public void UpdateNote(string clip, float volume, float r, float g, float b)
    {
        NoteObject noteObject = GetComponent<GNote>();
        noteObject.note = new Note { audioClip = AudioClipDictionnary.audioClips[clip], volume = volume };
        GetComponent<MeshRenderer>().material.color = new Color(r, g, b);
        AudioSource source = GetComponent<AudioSource>();
        source.clip = noteObject.note.audioClip;
        source.volume = noteObject.note.volume;
        source.Play();
    }

    //Call on every client when someone grab a note. Forbid a client to grab a note already held by another one
    [PunRPC]
    public void UpdateIsGrabbed()
    {
        GetComponent<GNote>().IsGrabbed = !GetComponent<GNote>().IsGrabbed;
    }
}

