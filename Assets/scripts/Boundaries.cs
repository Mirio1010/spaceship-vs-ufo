using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    [Header("Camera Bounds Offset")]
    public float xPadding = 0.5f; // space from screen edges
    public float yPadding = 0.5f; // optional, in case you want to limit top/bottom

    private float xMin, xMax, yMin, yMax;

    void Start()
    {
        // Get the camera boundaries in world space
        Camera cam = Camera.main;

        // Distance between camera and player
        float distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, distanceZ));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, distanceZ));

        xMin = bottomLeft.x + xPadding;
        xMax = topRight.x - xPadding;

        yMin = bottomLeft.y + yPadding;
        yMax = topRight.y - yPadding;
    }

    void LateUpdate()
    {
        // Clamp the position to stay within screen limits
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, xMin, xMax);
        pos.y = Mathf.Clamp(pos.y, yMin, yMax);
        transform.position = pos;
    }
}

