using UnityEngine;
using System.Collections;

public class UFOSpawner : MonoBehaviour
{
    [Header("Prefab & Position")]
    public GameObject ufoPrefab;
    public Vector2 spawnPosition = new Vector2(0f, 3f);

    [Header("Spawn Timing")]
    public float startDelay = 1.0f;
    public float currentDelay = 2.0f;
    public float minDelay = 0.5f;
    public float delayDecrement = 0.15f;

    [Header("Difficulty (Speed)")]
    public float startSpeed = 3.0f;
    public float currentSpeed = 3.0f;
    public float maxSpeed = 8.0f;
    public float speedIncrement = 0.5f;

    [Header("Step Down Ramp")]
    public bool rampStepDown = true;
    public float startStepDown = 0.5f;
    public float stepDownIncrement = 0.05f;
    public float maxStepDown = 1.5f;

    void Start()
    {
        currentSpeed = startSpeed;
        StartCoroutine(FirstSpawn());
    }

    IEnumerator FirstSpawn()
    {
        yield return new WaitForSeconds(startDelay);
        SpawnUFO();
    }

    // ⬇️ Your method goes HERE, inside the class ⬇️
    public void SpawnUFO()
    {
        // Choose a random X within camera view
        Camera cam = Camera.main;
        float distanceZ = Mathf.Abs(cam.transform.position.z);
        Vector3 leftEdge = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, distanceZ));
        Vector3 rightEdge = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, distanceZ));
        float randomX = Random.Range(leftEdge.x + 0.5f, rightEdge.x - 0.5f); // padding so it doesn’t clip edges

        // Use that X, keep the Y from spawnPosition
        Vector2 randomSpawn = new Vector2(randomX, spawnPosition.y);

        // Create the UFO
        var ufo = Instantiate(ufoPrefab, randomSpawn, Quaternion.identity);

        // Apply current difficulty settings
        if (ufo.TryGetComponent<UFOMoverInvader>(out var mover))
        {
            mover.moveSpeed = currentSpeed;

            if (rampStepDown)
            {
                mover.stepDownAmount = Mathf.Min(
                    startStepDown + Mathf.Max(0f, (currentSpeed - startSpeed) / speedIncrement) * stepDownIncrement,
                    maxStepDown
                );
            }
        }
    }

    public void UFOShot()
    {
        // Ramp difficulty for the NEXT spawn
        currentSpeed = Mathf.Min(currentSpeed + speedIncrement, maxSpeed);
        currentDelay = Mathf.Max(currentDelay - delayDecrement, minDelay);
        StartCoroutine(RespawnAfterDelay());
    }

    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(currentDelay);
        SpawnUFO();
    }
}

