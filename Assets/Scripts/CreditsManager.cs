using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditsManager : MonoBehaviour
{
    public void BackToMenu()
    {
        // Load the main menu scene when the Back button is clicked
        SceneManager.LoadScene("Menu");
    }
}
