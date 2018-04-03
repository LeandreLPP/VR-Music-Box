using UnityEngine;

public class HatCymbalPedal : MonoBehaviour {

    GameObject charleston;
    Charleston script;

	// Use this for initialization
	void Start () {
        charleston = GameObject.FindGameObjectWithTag("Charleston");
        script = charleston.GetComponent<Charleston>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        script.SetIsOpen(false);
    }

    private void OnTriggerExit(Collider other)
    {
        script.SetIsOpen(true);
    }
}
