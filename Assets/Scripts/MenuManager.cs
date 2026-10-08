using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        // Load the game scene Level1 when the Play button is clicked
        SceneManager.LoadScene("LevelSelector");
    }
    public void SelectCharacter()
    {
        // Load the character selection scene when the Select Character button is clicked
        SceneManager.LoadScene("CharacterSelection");
    }
    public void QuitGame()
    {
        // Quit the application when the Quit button is clicked
        Application.Quit();
    }
    public void GoToCredits()
    {
        // Load the credits scene when the Credits button is clicked
        SceneManager.LoadScene("Credits");
    }
}
