using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenuController : MonoBehaviour
{

    public GameObject mainCanvas;
    public GameObject optionsCanvas;

    public Button SetPlayerModeBtt;

    public void Update()
    {
        SetPlayerModeBtt.GetComponentInChildren<TextMeshProUGUI>().text = MainMenuController.playerMode.ToString();
    }

    public void GoToMainMenu()
    {
        if (mainCanvas != null && optionsCanvas != null)
        {
            optionsCanvas.SetActive(false);
            mainCanvas.SetActive(true);
        }
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
