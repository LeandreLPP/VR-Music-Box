public class PhotonToggle : GToggle {

    /*public override void ClickOn(BaseEventData data)
    {
        base.ClickOn(data);
        transform.parent.parent.parent.GetComponent<PhotonView>().RPC("UpdateToggle", PhotonTargets.OthersBuffered,Step.StepNumber, Height);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        transform.parent.parent.parent.GetComponent<PhotonView>().RPC("UpdateToggle", PhotonTargets.OthersBuffered, Step.StepNumber, Height);
    }*/

    public override void Toggle()
    {
        base.Toggle();
        transform.parent.parent.parent.GetComponent<PhotonView>().RPC("UpdateToggle", PhotonTargets.OthersBuffered, Step.StepNumber, Height, State);
    }
}
