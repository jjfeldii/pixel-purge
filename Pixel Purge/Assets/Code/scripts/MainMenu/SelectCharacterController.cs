using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectCharacterController : MonoBehaviour
{
    public static SelectCharacterController instance;

    public GameObject mainCanvas;
    public GameObject characterSelectCanvas;

    public Button setPrevColor;
    public Button setNextColor;

    public Image circleImage;
    private Color[] colors = new Color[10];
    public int colorIdx;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        //add Colors
        colors[0] = new Color(0.86f, 0.44f, 0.44f); // Soft Red
        colors[1] = new Color(0.56f, 0.86f, 0.56f); // Soft Green
        colors[2] = new Color(0.53f, 0.66f, 0.87f); // Soft Blue
        colors[3] = new Color(0.95f, 0.87f, 0.58f); // Soft Yellow
        colors[4] = new Color(0.71f, 0.57f, 0.84f); // Soft Purple
        colors[5] = new Color(0.65f, 0.80f, 0.88f); // Soft Sky Blue
        colors[6] = new Color(0.75f, 0.75f, 0.75f); // Soft Gray
        colors[7] = new Color(0.93f, 0.64f, 0.38f); // Soft Orange
        colors[8] = new Color(0.96f, 0.75f, 0.75f); // Soft Pink
        colors[9] = new Color(0.50f, 0.76f, 0.76f); // Soft Teal

        colorIdx = 0;
        circleImage.color = colors[colorIdx];
        UpdateButtonStates();
    }


    public void GoToMainMenu()
    {
        if (mainCanvas != null && characterSelectCanvas != null)
        {
            PlayerPrefs.SetString("SelectedColor", ColorUtility.ToHtmlStringRGB(circleImage.color));
           
            characterSelectCanvas.SetActive(false);
            mainCanvas.SetActive(true);
        }
    }

    public void SetNextColor()
    {
        if (colorIdx < colors.Length - 1) // Ensure we are not out of bounds
        {
            colorIdx++;
            circleImage.color = colors[colorIdx];
            UpdateButtonStates();
        }
    }

    public void SetPrevColor()
    {
        if (colorIdx > 0) // Ensure we are not out of bounds
        {
            colorIdx--;
            circleImage.color = colors[colorIdx];
            UpdateButtonStates();
        }
    }

    private void UpdateButtonStates()
    {
        // Enable or disable buttons based on the current color index
        setPrevColor.gameObject.SetActive(colorIdx > 0);
        setNextColor.gameObject.SetActive(colorIdx < colors.Length - 1);
    }
}
