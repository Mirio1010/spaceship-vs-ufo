using UnityEngine;

public class PinHit : MonoBehaviour
{
    public string targetTag = "UFO";

    [Header("Scoring")]
    public int pointsForUFO = 100;   // <-- Define your points here

    [Header("Audio")]
    public AudioClip popSfx;         // assign a clip in Inspector
    public float sfxVolume = 0.85f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            // Play sound
            if (popSfx)
                AudioSource.PlayClipAtPoint(popSfx, transform.position, sfxVolume);

            // Add score
            if (ScoreManager.Instance != null)
                ScoreManager.Instance.Add(pointsForUFO);

            // Notify spawner to respawn after delay
            var spawner = FindObjectOfType<UFOSpawner>();
            if (spawner != null)
                spawner.UFOShot();

            // Destroy UFO and the Pin
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
