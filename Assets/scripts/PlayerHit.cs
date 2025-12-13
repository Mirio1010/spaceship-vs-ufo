using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public AudioClip deathSfx;
    public float deathVolume = 0.9f;

    private bool isDead = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Absolute guard
        if (isDead)
            return;

        if (!other.CompareTag("UFO"))
            return;

        isDead = true;

        if (deathSfx)
            AudioSource.PlayClipAtPoint(deathSfx, transform.position, deathVolume);

        AudioListener.pause = true;

        // 🔑 DISABLE EVERY COLLIDER IMMEDIATELY
        foreach (Collider2D c in GetComponentsInChildren<Collider2D>())
            c.enabled = false;

        // Hide sprite
        var sr = GetComponentInChildren<SpriteRenderer>();
        if (sr) sr.enabled = false;

        // Call ONCE
        FindObjectOfType<GameManager>().PlayerDied();
    }
}
