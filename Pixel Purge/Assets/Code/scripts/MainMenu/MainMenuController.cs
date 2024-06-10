using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerMode { Singleplayer, Multiplayer }

public class MainMenuController : MonoBehaviour
{
    public static PlayerMode playerMode = PlayerMode.Singleplayer;

    public GameObject mainCanvas;
    public GameObject optionsCanvas;
    public GameObject characterSelectCanvas;

    public static void PlayGame()
    {
        SceneManager.LoadScene("BasementMain");
    }

    public void GoToOptionsMenu()
    {
        if (mainCanvas != null && optionsCanvas != null)
        {
            mainCanvas.SetActive(false);
            optionsCanvas.SetActive(true);
        }
    }

    public void GoToSelectCharacter()
    {
        if (mainCanvas != null && characterSelectCanvas != null)
        {
            mainCanvas.SetActive(false);
            characterSelectCanvas.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
