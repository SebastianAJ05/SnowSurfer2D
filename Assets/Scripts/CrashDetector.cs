using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private ParticleSystem crashEffect; // Particle effect to play when the player crashes
    [SerializeField] private float crashDelay = 1f; // Reference to the crashDelay script

    PlayerController playerController;

    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");

        if (other.gameObject.layer == layerIndex)
        {
            playerController.CanControlPlayer = false; // Disable player control
            Debug.Log("Player has crashed!");
            crashEffect.Play(); // Play the crash effect particles
            Invoke(nameof(ReloadScene), crashDelay); // Reload the scene after the specified delay
        }
    }

    void ReloadScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
