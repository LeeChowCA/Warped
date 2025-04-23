using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPop : MonoBehaviour
{
    [SerializeField] private GameObject popupUI; // Reference to the popup UI (e.g., a Panel)
    [SerializeField] private float reloadDelay = 0.5f;
    [SerializeField] private float showDelay = 0.5f;
    private void Start()
    {
        popupUI.SetActive(false); // Ensure the popup is hidden at the start
    }

    public void ShowWithDelay()
    {
        Invoke(nameof(Show), showDelay); // Delay the execution of the Show method
    }

    private void Show()
    {
        popupUI.SetActive(true); // Show the popup
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    //private void ReloadScene()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    //}

    public void QuitGame()
    {
        Time.timeScale = 1f; // Resume the game
        Application.Quit(); // Quit the application
        Debug.Log("Game Quit"); // This will show in the editor console
    }
}
