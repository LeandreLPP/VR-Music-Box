using UnityEngine;
using System.Collections;

public class VibraphoneSequencerSource : SequencerNoteSource
{

    public float place;

    protected Animator animator;
    public Color color;
    public Vector3 vect;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        float frequency = Mathf.PI * 2f/ ((transform.parent.childCount - 1f) / 3f);
        float r = (Mathf.Sin(frequency * place) + 1f) / 2f;
        float g = (Mathf.Sin((frequency * place) + 2f) + 1f) / 2f; // ((2f * Mathf.PI) / 3f));
        float b = (Mathf.Sin((frequency * place) + 4f) + 1f) / 2f; // ((4f * Mathf.PI) / 3f));

        vect = new Vector3(r, g, b);
        color = new Color(vect.x, vect.y, vect.z);

        GetComponent<Renderer>().material.color = color;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("HeadStick"))
            return;

        if (other.GetComponent<ComputeVelocity>().velocity.y < 0)
        {
            Play();
        }
    }


    public override void Play()
    {
        base.Play();
        animator.SetTrigger("play");
    }

    protected override NoteObject InstantiateNoteObject()
    {
        var no = Instantiate<GameObject>(noteObjectPrefab.gameObject).GetComponent<NoteObject>();
        var renderer = no.gameObject.GetComponent<Renderer>();

        renderer.material.color = color;
        return no;
    }
}
