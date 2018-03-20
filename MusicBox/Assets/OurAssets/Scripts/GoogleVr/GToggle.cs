using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GToggle : VisualSequencerToggle {

    public virtual void ClickOn(BaseEventData data)
    {
        GetComponent<VisualSequencerToggle>().Toggle();
    }
}
