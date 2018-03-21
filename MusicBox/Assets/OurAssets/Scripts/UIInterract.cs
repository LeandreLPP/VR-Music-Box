using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInterract : MonoBehaviour {

    public GameObject laserPrefab;
    public LayerMask UILayer;

    protected GameObject laser;
    protected ControllerGrabber grabber;

    protected PointerEventData pointer;

    private Button button;
    protected Button Button
    {
        get
        {
            return button;
        }
        set
        {
            SimulateUnhover();
            button = value;
            SimulateHover();
        }
    }

    #region SteamVR Stuff
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    #endregion
    
    void Start ()
    {
        grabber = GetComponent<ControllerGrabber>();
        laser = Instantiate(laserPrefab);

        pointer = new PointerEventData(EventSystem.current); // pointer event for Execute
    }

    private void ShowLaser(RaycastHit hit)
    {
        var hitPoint = hit.point;
        laser.SetActive(true);

        laser.transform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);

        laser.transform.LookAt(hitPoint);

        laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y,
            hit.distance);
    }

    private void HideLaser()
    {
        laser.SetActive(false);
    }

    private void SimulateHover()
    {
        if (Button == null) return;

        ExecuteEvents.Execute(Button.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
    }
    private void SimulateUnhover()
    {
        if (Button == null) return;
        
        ExecuteEvents.Execute(Button.gameObject, pointer, ExecuteEvents.pointerExitHandler);
    }
    private void SimulateClickDown()
    {
        if (Button == null) return;

        ExecuteEvents.Execute(Button.gameObject, pointer, ExecuteEvents.pointerDownHandler);
    }
    private void SimulateClickUp()
    {
        if (Button == null) return;

        ExecuteEvents.Execute(Button.gameObject, pointer, ExecuteEvents.pointerUpHandler);
    }
    private void SimulateClick()
    {
        if (Button == null) return;

        ExecuteEvents.Execute(Button.gameObject, pointer, ExecuteEvents.pointerClickHandler);
    }

    private void Update()
    {

        RaycastHit hit;
        if (Physics.Raycast(trackedObj.transform.position, trackedObj.transform.forward, out hit, 500f, UILayer.value))
        {
            ShowLaser(hit);
            var go = hit.collider.gameObject;
            var but = go.GetComponent<Button>();

            if (but != Button)
                Button = but;
        }
        else
        {
            HideLaser();
            Button = null;
        }

        if (Controller.GetHairTriggerDown())
        {
            SimulateClick();
            SimulateClickDown();
        }

        if (Controller.GetHairTriggerUp())
            SimulateClickUp();
    }
}
