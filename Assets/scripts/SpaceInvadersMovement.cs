using UnityEngine;

public class UFOMoverInvader : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveSpeed = 3f;      // side-to-side speed
    public float xPadding = 0.5f;     // extra gap from edges
    public bool moveRight = true;     // starting direction

    [Header("Vertical Step")]
    public float stepDownAmount = 0.5f;

    // cached camera/world edges and sprite half-width
    float worldLeftEdge, worldRightEdge;
    float halfWidth;

    void Start()
    {
        // Find sprite (or renderer) width in world units
        var sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            halfWidth = sr.bounds.extents.x; // half of the rendered width
        }
        else
        {
            // Fallback if no SpriteRenderer found
            halfWidth = 0.5f;
        }

        // Compute world-space edges based on the main camera
        Camera cam = Camera.main;
        // Distance from camera to this object along Z (2D: usually ~10)
        float z = Mathf.Abs(cam.transform.position.z - transform.position.z);

        Vector3 left = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, z));
        Vector3 right = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, z));

        worldLeftEdge  = left.x  + xPadding;
        worldRightEdge = right.x - xPadding;
    }

    void Update()
    {
        float dir = moveRight ? 1f : -1f;
        Vector3 pos = transform.position;

        // Move horizontally
        pos += Vector3.right * dir * moveSpeed * Time.deltaTime;

        // Compute the UFO's visual edges using halfWidth
        float leftEdgeOfUFO  = pos.x - halfWidth;
        float rightEdgeOfUFO = pos.x + halfWidth;

        // If the UFO's visual edge hits/passes the screen bounds, snap, step, and flip
        if (moveRight && rightEdgeOfUFO >= worldRightEdge)
        {
            pos.x = worldRightEdge - halfWidth; // snap so it's just inside
            StepDownAndFlip(ref pos);
        }
        else if (!moveRight && leftEdgeOfUFO <= worldLeftEdge)
        {
            pos.x = worldLeftEdge + halfWidth; // snap so it's just inside
            StepDownAndFlip(ref pos);
        }

        transform.position = pos;
    }

    void StepDownAndFlip(ref Vector3 pos)
    {
        pos += Vector3.down * stepDownAmount;
        moveRight = !moveRight;
        // Optional: tiny nudge so we don't immediately retrigger the same edge
        pos.x += moveRight ? 0.001f : -0.001f;
    }
}


