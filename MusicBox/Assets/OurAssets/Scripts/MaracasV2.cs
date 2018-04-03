using UnityEngine;

public class MaracasV2 : MonoBehaviour {

    Rigidbody rigidbody;
    float oldPosition;
    float oldVelocity;
    AudioSource source;
    float threshold;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        threshold = 0.15f;

    }
	
	// Update is called once per frame
	void Update () {
        float currentVelocity = (transform.position.y - oldPosition) / Time.deltaTime;
        //Debug.Log(currentVelocity);
        if (Mathf.Abs(currentVelocity) > threshold){
            if ((oldVelocity * currentVelocity) < 0)
                source.Play();
            oldPosition = transform.position.y;
            oldVelocity = currentVelocity;
        }

    }
}
