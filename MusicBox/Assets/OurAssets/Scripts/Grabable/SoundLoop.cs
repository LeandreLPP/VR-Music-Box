﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLoop : BaseGrabable {

    public AudioClip clip;
    public GameObject laserPrefab;

    private GameObject laser;

    private ViveTrackerMusic musicBoxTargeted = null;
    private ViveTrackerMusic MusicBoxTargeted
    {
        get
        {
            return musicBoxTargeted;
        }

        set
        {
            if(musicBoxTargeted && musicBoxTargeted != value)
                musicBoxTargeted.Pointed = false;
            musicBoxTargeted = value;
            if (value)
                musicBoxTargeted.Pointed = true;
        }
    }

    private ViveTrackerMusic MusicBoxAssignated { get; set; }
    
    protected override void OnRelease(AGrabber grabber)
    {
        if(MusicBoxTargeted)
        {
            MusicBoxAssignated = MusicBoxTargeted;
            MusicBoxTargeted = null;
            MusicBoxAssignated.ChangeSound(clip);
            var joint = GetComponent<SpringJoint>();
            joint.connectedBody = MusicBoxAssignated.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void Start()
    {
        laser = Instantiate(laserPrefab);
    }

    private void ShowLaser(Vector3 hitpoint)
    {
        laser.SetActive(true);

        laser.transform.position = Vector3.Lerp(transform.position, hitpoint, .5f);

        laser.transform.LookAt(hitpoint);
        var distance = (hitpoint - transform.position).magnitude;

        laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, distance);
    }

    private void Update()
    {
        if (IsGrabbed)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Grabber.Forward, out hit, 100))
            {
                ShowLaser(hit.point);
                GameObject objectHit = hit.collider.gameObject;
                MusicBoxTargeted = objectHit.GetComponent<ViveTrackerMusic>();
            }
            else
            {
                MusicBoxTargeted = null;
                laser.SetActive(false);
            }
        }
        else
        {
            MusicBoxTargeted = null;
            laser.SetActive(false);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.GetComponent<ViveTrackerMusic>() && MusicBoxAssignated == other.GetComponent<ViveTrackerMusic>())
            Reset();
    }

    private void Reset()
    {
        MusicBoxAssignated = null;
        GetComponent<SpringJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
        transform.localPosition = new Vector3(0, 0, 0);
    }
}