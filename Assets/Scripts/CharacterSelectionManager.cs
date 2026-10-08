using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectiojnManager : MonoBehaviour
{
    // Save the selected character index
    /// <summary>
    /// Saves the selected character index to PlayerPrefs.
    /// </summary>
    /// <param name="characterIndex"></param>

    public void SelectCharacter(int characterIndex)
    {
        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
        PlayerPrefs.Save();
        //Back to Menu
        SceneManager.LoadScene("Menu");
    }
}
