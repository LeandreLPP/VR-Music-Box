using UnityEngine.EventSystems;

public class GToggle : VisualSequencerToggle {

    public virtual void ClickOn(BaseEventData data)
    {
        Toggle();
    }
}
