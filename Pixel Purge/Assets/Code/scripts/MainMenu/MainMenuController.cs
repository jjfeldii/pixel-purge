using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerMode { Singleplayer, Multiplayer }

public class MainMenuController : MonoBehaviour
{
    public static PlayerMode playerMode = PlayerMode.Singleplayer;

    public static void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GoToOptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
