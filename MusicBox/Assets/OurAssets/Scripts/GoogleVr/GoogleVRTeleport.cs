using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoogleVRTeleport : MonoBehaviour
{
    public const int TpTime = 10;
    public GameObject player;
    private int tmp;
    private bool click;


    void Start()
    {
        tmp = 0;
        click = false;
    }

    void Update()
    {
        if(click)
            tmp++;
    }

    public void PointerDown(BaseEventData data)
    {
        click = true;
    }

    public void PointerUp(BaseEventData data)
    {
        if (tmp >= TpTime)
            TeleportTo(data);
        tmp = 0;
        click = false;
    }


    public void TeleportTo(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        Vector3 worldPos = pointerData.pointerCurrentRaycast.worldPosition;
        Vector3 playerPos = new Vector3(worldPos.x, player.transform.position.y, worldPos.z);
        player.transform.position = playerPos;
    }
}
