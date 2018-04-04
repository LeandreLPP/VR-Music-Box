using UnityEngine.EventSystems;

public class GToggle : SequencerToggleUI {

    public virtual void ClickOn(BaseEventData data)
    {
        Toggle();
    }
}
