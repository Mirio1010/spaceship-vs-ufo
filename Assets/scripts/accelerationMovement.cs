using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerThrust2D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float acceleration = 18f;   // how quickly you speed up
    [SerializeField] float maxSpeed     = 7f;    // top speed
    [SerializeField] float decelRate    = 20f;   // how quickly you slow when no input

    [Header("Input")]
    [SerializeField] bool useRawInput   = true;  // Raw = snappy, non-raw = smoother input

    [Header("Visuals (optional)")]
    [SerializeField] SpriteRenderer sr;          // assign if you want auto-flip

    Rigidbody2D rb;
    Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!sr) sr = GetComponent<SpriteRenderer>();

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // smoother visuals
    }

    void Update()
    {
        float x = useRawInput ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal");
        float y = useRawInput ? Input.GetAxisRaw("Vertical")   : Input.GetAxis("Vertical");
        input = new Vector2(x, y);

        // Optional: flip sprite based on travel direction when moving horizontally
        if (sr && Mathf.Abs(rb.linearVelocity.x) > 0.05f)
            sr.flipX = rb.linearVelocity.x < 0f;
    }

    void FixedUpdate()
    {
        // If we have input, apply acceleration in that direction
        if (input.sqrMagnitude > 0.0001f)
        {
            Vector2 dir = input.normalized;
            rb.AddForce(dir * acceleration, ForceMode2D.Force);

            // Clamp top speed
            if (rb.linearVelocity.sqrMagnitude > maxSpeed * maxSpeed)
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
        else
        {
            // No input? Gently slow down
            if (rb.linearVelocity.sqrMagnitude > 0.0001f)
            {
                Vector2 v = Vector2.MoveTowards(rb.linearVelocity, Vector2.zero, decelRate * Time.fixedDeltaTime);
                rb.linearVelocity = v;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }
}
