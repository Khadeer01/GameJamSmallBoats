using UnityEngine;
using UnityEngine.SceneManagement; 
public class MainMenuManager : MonoBehaviour
{
    // Load the main game scene
    public void PlayGame()
    {
        
        SceneManager.LoadScene("");
    }

    // Quit the application
    public void QuitGame()
    {
        Debug.Log("Quit Game"); 
        Application.Quit();
    }
}
