using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GBackground : MonoBehaviour {

    public void ClickOnBackground(BaseEventData data)
    {
        NoteObject note = GvrPointerInputModule.Pointer.PointerTransform.GetComponentInChildren<NoteObject>();
        note.gameObject.layer = LayerMask.NameToLayer("Default");
        note.Release(null);
        if (note)
            note.transform.SetParent(null, true);
        else
            return;
   
    }
}
