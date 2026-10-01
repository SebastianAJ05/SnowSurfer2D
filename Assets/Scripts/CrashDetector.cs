using UnityEngine;

public class CrashDetector : MonoBehaviour
{
        [SerializeField] private ParticleSystem deathEffect; // Particle effect to play when the player crashes
    void OnTriggerEnter2D(Collider2D other)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");

        if (other.gameObject.layer == layerIndex)
        {
            Debug.Log("Player has crashed!");
            deathEffect.Play(); // Play the death effect particles
        }
    }
}
