using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the highest unlocked level from PlayerPrefs, default to 1 if not set
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        Transform levels = transform.GetChild(0); // Assuming the levels are children of the first child of this GameObject

        for (int i = 0; i < levels.childCount; i++)
        {
            Button levelButton = levels.GetChild(i).GetComponent<Button>();
            levelButton.interactable = (i < unlockedLevel); // Enable button if level is unlocked, otherwise disable it
        }
    }

    void GoToLevel(int level)
    {
        // Load the selected level scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + level);
    }

}
