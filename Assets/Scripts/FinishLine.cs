using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 1f; // Delay before reloading the scene
    [SerializeField] private ParticleSystem finishEffect; // Particle effect to play when the player crosses the finish line
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has crossed the finish line!");
            finishEffect.Play(); // Play the finish effect particles
            Invoke(nameof(NextLevel), reloadDelay); // Load the next level after the specified delay
        }
    }
    void NextLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel + 1); // Unlock the next level
        PlayerPrefs.Save();
        //Reload the current scene
        if (unlockedLevel > 6)
        {
            SceneManager.LoadScene("Menu"); // If the last level is completed, go back to the menu
        }
        else
        {
            SceneManager.LoadScene($"Level{unlockedLevel + 1}"); // Load the next level
        }
        
    }
}
