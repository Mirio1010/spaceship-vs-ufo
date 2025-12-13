using UnityEngine;

public class UFOBoundary : MonoBehaviour
{
    [Header("Boundary Padding")]
    public float xPadding = 0.5f;
    public float yPadding = 0.5f;

    private float xMin, xMax, yMin, yMax;

    void Start()
    {
        Camera cam = Camera.main;
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
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, xMin, xMax);
        pos.y = Mathf.Clamp(pos.y, yMin, yMax);
        transform.position = pos;
    }
}
