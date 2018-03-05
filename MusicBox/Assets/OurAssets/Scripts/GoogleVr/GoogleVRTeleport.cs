using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class GoogleVRTeleport : MonoBehaviour
{
    private Vector3 startingPosition;
    private Renderer renderer;
    public GameObject player;
    public Material inactiveMaterial;
    public Material gazedAtMaterial;

    void Start()
    {
        startingPosition = transform.localPosition;
        renderer = GetComponent<Renderer>();
        SetGazedAt(false);
    }

    public void SetGazedAt(bool gazedAt)
    {
        if (inactiveMaterial != null && gazedAtMaterial != null)
        {
            renderer.material = gazedAt ? gazedAtMaterial : inactiveMaterial;
            return;
        }
    }

    public void Reset()
    {
        int sibIdx = transform.GetSiblingIndex();
        int numSibs = transform.parent.childCount;
        for (int i = 0; i < numSibs; i++)
        {
            GameObject sib = transform.parent.GetChild(i).gameObject;
            sib.transform.localPosition = startingPosition;
            sib.SetActive(i == sibIdx);
        }
    }

    public void Recenter()
    {
#if !UNITY_EDITOR
        GvrCardboardHelpers.Recenter();
#else
      if (GvrEditorEmulator.Instance != null) {
        GvrEditorEmulator.Instance.Recenter();
      }
#endif  // !UNITY_EDITOR
    }

    public void TeleportTo(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        Vector3 worldPos = pointerData.pointerCurrentRaycast.worldPosition;
        Vector3 playerPos = new Vector3(worldPos.x, player.transform.position.y, worldPos.z);
        player.transform.position = playerPos;
    }
}
