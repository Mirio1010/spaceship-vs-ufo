using UnityEngine;

public class PinMover : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction = Vector2.up;
    public float lifetime = 5f;

    void OnEnable() { Destroy(gameObject, lifetime); }

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    public void Fire(Vector2 dir, float spd)
    {
        direction = dir.normalized;
        speed = spd;
        enabled = true;
    }
}


