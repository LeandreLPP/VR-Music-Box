public class PhotonToggle : GToggle {

    public override void Toggle()
    {
        base.Toggle();
        transform.parent.parent.parent.GetComponent<PhotonView>().RPC("UpdateToggle", PhotonTargets.Others, Step.StepNumber, Height, State);
    }
}
