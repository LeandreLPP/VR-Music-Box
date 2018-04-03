using UnityEngine;
using UnityEngine.EventSystems;

public class GoogleVibraphone : MonoBehaviour
{

    public void OnClick (BaseEventData data) {
       GetComponent<SequencerNoteSource>().Play();
    }
}
