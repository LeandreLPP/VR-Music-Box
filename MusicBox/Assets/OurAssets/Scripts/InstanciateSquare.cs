using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciateSquare : MonoBehaviour {

    public int maxHeight = 2;
    public int step = 10;
    private const float heightOffSet = 0.2f;
    public GameObject squarePrefab;
    public float factor = 1;

    // Use this for initialization
    void Start () {
		for(int i = 0; i < 360; i +=step)
        {
            for(int j = 0; j < maxHeight; j++)
            {
                GameObject square = Instantiate(squarePrefab);
                Quaternion rot = Quaternion.AngleAxis(i, Vector3.up);
                square.transform.position = rot * Vector3.forward * factor + Vector3.up * j * heightOffSet + Vector3.up*0.5f;
                square.transform.rotation = rot;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
