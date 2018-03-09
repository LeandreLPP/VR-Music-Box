using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] int step = 10;
    [SerializeField] float factor = 100f;


    void Start()
    {
        for (var i = 0; i < 360; i+= step)
        {

            var rot = Quaternion.AngleAxis(i, Vector3.up);
            var pos = rot * Vector3.forward * factor;
            var go = Instantiate(_prefab, pos, rot);
            var dancer = go.GetComponent<Puppet.Dancer>();

            dancer.footDistance  *= Random.Range(0.8f, 2.0f);
            dancer.stepFrequency *= Random.Range(0.4f, 1.6f);
            dancer.stepHeight    *= Random.Range(0.75f, 1.25f);
            dancer.stepAngle     *= Random.Range(0.75f, 1.25f);

            dancer.hipHeight        *= Random.Range(0.75f, 1.25f);
            dancer.hipPositionNoise *= Random.Range(0.75f, 1.25f);
            dancer.hipRotationNoise *= Random.Range(0.75f, 1.25f);

            dancer.spineBend           = Random.Range(4.0f, -16.0f);
            dancer.spineRotationNoise *= Random.Range(0.75f, 1.25f);

            dancer.handPositionNoise *= Random.Range(0.5f, 2.0f);
            dancer.handPosition      += Random.insideUnitSphere * 0.25f;

            dancer.headMove       *= Random.Range(0.2f, 2.8f);
            dancer.noiseFrequency *= Random.Range(0.4f, 1.8f);
            dancer.randomSeed      = Random.Range(0, 0xffffff);

            var renderer = dancer.GetComponentInChildren<Renderer>();
            renderer.material.color = Random.ColorHSV(0, 1, 0.6f, 0.8f, 0.8f, 1.0f);
        }
    }
}
