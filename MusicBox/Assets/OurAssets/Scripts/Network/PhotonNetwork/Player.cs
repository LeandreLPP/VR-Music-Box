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
            transform.position = Camera.position;
            transform.localPosition = new Vector3(0, 0.1f, 0);
        }
    }

    protected void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, CorrectNotePos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, CorrectNoteRot, Time.deltaTime * 5);
        }
        else
        {
            //When a player disconnect, we just want to destroy his playerPrefab, so we check when the owner field becomes null
            if (GetComponent<PhotonView>().owner == null)
            {
                PhotonNetwork.Destroy(gameObject);
            }

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
