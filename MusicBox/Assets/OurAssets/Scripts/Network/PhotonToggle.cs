using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhotonToggle : GToggle {

    public override void ClickOn(BaseEventData data)
    {
        base.ClickOn(data);
        transform.parent.parent.parent.GetComponent<PhotonView>().RPC("UpdateToggle", PhotonTargets.OthersBuffered,Step.StepNumber, Height);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        transform.parent.parent.parent.GetComponent<PhotonView>().RPC("UpdateToggle", PhotonTargets.OthersBuffered, Step.StepNumber, Height);
    }

}
