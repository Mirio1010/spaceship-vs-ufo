using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHit : MonoBehaviour
{
    [Header("Death Feedback (optional)")]
    public AudioClip deathSfx;
    public float deathVolume = 0.9f;

    [Tooltip("Seconds to wait before restart")]
    public float restartDelay = 0.75f;

    bool isDead = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only react to UFOs
        if (!isDead && other.CompareTag("UFO"))
        {
            isDead = true;

            // Optional: sound at player position
            if (deathSfx) AudioSource.PlayClipAtPoint(deathSfx, transform.position, deathVolume);

            // Optional: hide sprite/collisions immediately so you don't 'hit' twice
            var sr = GetComponentInChildren<SpriteRenderer>();
            if (sr) sr.enabled = false;
            var col = GetComponent<Collider2D>();
            if (col) col.enabled = false;

            // Destroy the player object after a short delay (lets the sound play)
            Destroy(gameObject, 0.1f);

            // Restart the scene (simple arcade loop)
            Invoke(nameof(RestartLevel), restartDelay);
        }
    }

    void RestartLevel()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
}
