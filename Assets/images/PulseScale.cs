using UnityEngine;

public class PulseScale : MonoBehaviour
{
    public float scaleAmount = 0.05f;   // how big the pulse is
    public float speed = 2f;             // how fast it pulses

    Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * speed) * scaleAmount;
        transform.localScale = startScale * scale;
    }
}
