using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GoogleController : EventTrigger
{
    public GameObject player;


    public override void OnPointerDown(PointerEventData data)
    {
        base.OnPointerDown(data);
        Debug.Log("EVENTDOWN");
    }

    public override void OnPointerUp(PointerEventData data)
    {
        base.OnPointerUp(data);
        Debug.Log("EVENTUP");
    }

    public override void OnPointerClick(PointerEventData data)
    {
        base.OnPointerClick(data);
        Debug.Log("EVENTCLICK");
        TeleportTo();
    }

    private void TeleportTo()
    {
        Debug.Log("TELEPORTTTTTTT");
        Vector3 worldPos = GvrPointerInputModule.Pointer.PointerTransform.position;
        Vector3 playerPos = new Vector3(worldPos.x, player.transform.position.y, worldPos.z);
        player.transform.position = playerPos;
    }
}
