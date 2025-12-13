using UnityEngine;
using System.Collections;
using TMPro;

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
    public float maxSpeed = 30.0f;
    public float speedIncrement = 0.5f;

    [Header("Level System")]
    public int killsToNextLevel = 5;
    public int maxLevel = 3;

    [Header("UI")]
    public TextMeshProUGUI levelText;   // 👈 DRAG YOUR LEVEL TEXT HERE

    private int currentLevel = 1;
    private int killsThisLevel = 0;

    private float baseSpeed;
    private float bonusSpeed;
    private float currentSpeed;

    void Start()
    {
        ApplyLevelDifficulty();
        UpdateLevelText();
        StartCoroutine(FirstSpawn());
    }

    IEnumerator FirstSpawn()
    {
        yield return new WaitForSeconds(startDelay);
        SpawnUFO();
    }

    void SpawnUFO()
    {
        Camera cam = Camera.main;
        float z = Mathf.Abs(cam.transform.position.z);

        Vector3 left = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, z));
        Vector3 right = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, z));

        float randomX = Random.Range(left.x + 0.5f, right.x - 0.5f);
        Vector2 spawnPos = new Vector2(randomX, spawnPosition.y);

        GameObject ufo = Instantiate(ufoPrefab, spawnPos, Quaternion.identity);

        if (ufo.TryGetComponent<UFOMoverInvader>(out var mover))
        {
            mover.moveSpeed = currentSpeed;
        }
    }

    // 🔥 CALLED WHEN A UFO IS DESTROYED
    public void UFOShot()
    {
        // Speed ramps slightly per kill
        bonusSpeed += speedIncrement;
        currentSpeed = Mathf.Min(baseSpeed + bonusSpeed, maxSpeed);

        // Spawn faster
        currentDelay = Mathf.Max(currentDelay - delayDecrement, minDelay);

        // Count kills toward next level
        killsThisLevel++;

        if (killsThisLevel >= killsToNextLevel && currentLevel < maxLevel)
        {
            currentLevel++;
            killsThisLevel = 0;
            ApplyLevelDifficulty();
            UpdateLevelText();
        }

        StartCoroutine(RespawnAfterDelay());
    }

    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(currentDelay);
        SpawnUFO();
    }

    void ApplyLevelDifficulty()
    {
        // Each level boosts base speed
        float levelMultiplier = 1f + (currentLevel - 1) * 0.75f;

        baseSpeed = Mathf.Min(startSpeed * levelMultiplier, maxSpeed);
        bonusSpeed = 0f;
        currentSpeed = baseSpeed;

        Debug.Log($"LEVEL {currentLevel} | Base Speed: {baseSpeed}");
    }

    void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = $"LEVEL {currentLevel}";
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}


