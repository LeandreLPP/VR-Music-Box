using UnityEngine;

public class Maracas : MonoBehaviour {

    public float threshold = 0.15f;

    float oldPosition;
    float oldVelocity;
    AudioSource source;
    new Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        float currentVelocity = (transform.position.y - oldPosition) / Time.deltaTime;

        if (Mathf.Abs(currentVelocity) > threshold)
        {
            if ((oldVelocity * currentVelocity) < 0)
                source.Play();

            oldPosition = transform.position.y;
            oldVelocity = currentVelocity;
        }
    }
}
