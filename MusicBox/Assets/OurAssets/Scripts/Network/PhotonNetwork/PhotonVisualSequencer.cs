public class PhotonVisualSequencer : Photon.PunBehaviour {

    //Method called by the client who change a toggle on all the others clients
    [PunRPC]
    public void UpdateToggle(int stepNumber, int height, bool state)
    {
        var toggle = GetComponent<SequencerUI>().StepsVisu[stepNumber].Toggles[height];
        if (toggle.State != state)
            toggle.Toggle();
    }


    //Method called by the HTC Vive client when someone change the tempo by rotating the tracker
    [PunRPC]
    public void UpdateTempo(float tempo)
    {
        GetComponent<Sequencer>().Tempo = tempo;
    }
}
