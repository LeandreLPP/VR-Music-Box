using UnityEngine;

public class PhotonVNote : PhotonNoteSynchro
{

    private PhotonSpawner spawner;

    private void Awake()
    {
            spawner = GameObject.FindGameObjectWithTag("PhotonSpawnerVibraphone").GetComponent<PhotonSpawner>();
    }

    [PunRPC]
    public void AddToSpwaner()
    {
        spawner.AddRPCNote(GetComponent<NoteObject>());
    }

    [PunRPC]
    public void RemoveToSpwaner(int index)
    {
        spawner.NotesHold.RemoveAt(index);
    }


}
