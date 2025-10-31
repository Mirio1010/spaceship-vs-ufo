using UnityEngine;

public class UFOMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;       // How fast the UFO moves
    public float moveDistance = 3f;    // How far it travels before turning
    public bool moveRight = true;      // Starting direction

    private Vector3 startPos;

    void Start()
    {
        // Remember starting position to calculate limits
        startPos = transform.position;
    }

    void Update()
    {
        // Move in the current direction
        float direction = moveRight ? 1f : -1f;
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // Check how far we've moved from the start
        if (moveRight && transform.position.x >= startPos.x + moveDistance)
        {
            moveRight = false; // switch direction to left
        }
        else if (!moveRight && transform.position.x <= startPos.x - moveDistance)
        {
            moveRight = true; // switch direction to right
        }
    }
}

