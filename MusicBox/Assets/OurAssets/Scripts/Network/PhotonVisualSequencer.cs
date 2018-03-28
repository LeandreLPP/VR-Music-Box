using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonVisualSequencer : Photon.PunBehaviour {

    //Method called by the client who change a toggle on all the others clients
    [PunRPC]
    public void UpdateToggle(int stepNumber, int height)
    {
        (GetComponent<VisualSequencer>().StepsVisu[stepNumber].Toggles[height] as VisualSequencerToggle).Toggle();
    }


    //Method called by the HTC Vive client when someone change the tempo by rotating the tracker
    [PunRPC]
    public void UpdateTempo(float tempo)
    {
        GetComponent<Sequencer>().Tempo = tempo;
    }
}
