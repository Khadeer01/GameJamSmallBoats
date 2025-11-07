using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // The name of the scene to load when Play is clicked
    [SerializeField] private string gameSceneName = "GameScene";

    // Called when the Play button is pressed
    public void PlayGame()
    {
        // Load the specified game scene
        SceneManager.LoadScene(gameSceneName);
    }

    // Called when the Quit button is pressed
    public void QuitGame()
    {
        Debug.Log("Quit Game"); 
        Application.Quit();     
    }
}
