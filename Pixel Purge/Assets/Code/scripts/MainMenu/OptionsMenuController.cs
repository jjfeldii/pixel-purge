using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class OptionsMenuController : MonoBehaviour
{

    public Button SetPlayerModeBtt;


    public void Update()
    {
        SetPlayerModeBtt.GetComponentInChildren<TextMeshProUGUI>().text = MainMenuController.playerMode.ToString();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ChangePlayerMode()
    {
        if (MainMenuController.playerMode == PlayerMode.Singleplayer) {
            MainMenuController.playerMode= PlayerMode.Multiplayer;
        } else {
            MainMenuController.playerMode = PlayerMode.Singleplayer;
        }
    }
}
