using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonExtended : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float triggerTime;
    public float delay;

    private float firstPressed;
    private float lastPressed;
    private bool buttonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        lastPressed = firstPressed = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    private void FixedUpdate()
    {
        if (!buttonPressed) return;

        var timePressed = Time.time - firstPressed;
        var timeSince = Time.time - lastPressed;

        if ((timePressed > triggerTime * 2 && timeSince > delay / 2f) || (timePressed > triggerTime && timeSince > delay))
        {
            GetComponent<Button>().onClick.Invoke();
            lastPressed = Time.time;
        }
    }
}
