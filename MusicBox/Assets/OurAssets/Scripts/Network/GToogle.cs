using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GToogle : MonoBehaviour
{

    public void ClickOn(BaseEventData data)
    {
        GetComponent<VisualSequencerToggle>().Toggle();
    }
}
