using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("Pin Setup")]
    public GameObject pinPrefab;
    public float pinSpeed = 12f;
    public Vector2 fireDirection = Vector2.up;

    [Header("Fire Input")]
    public string fireButton = "Fire1";

    [Header("Audio")]
    public AudioClip shootSfx;     // 🔊 Add this line
    public float shootVolume = 0.7f; // Optional, for volume control

    void Update()
    {
        if (Input.GetButtonDown(fireButton) || Input.GetKeyDown(KeyCode.Space))
        {
            var pin = Instantiate(pinPrefab, transform.position, Quaternion.identity);

            // Make it move
            if (pin.TryGetComponent<PinMover>(out var mover))
                mover.Fire(fireDirection, pinSpeed);

            // 🔊 Play sound at the player's position
            if (shootSfx)
                AudioSource.PlayClipAtPoint(shootSfx, transform.position, shootVolume);
        }
    }
}
