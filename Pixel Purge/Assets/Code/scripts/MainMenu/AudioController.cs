using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    public static AudioController instance;
    public Button AudioButton;

    private bool playing;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
        audioSource.clip = audioClip;
        audioSource.Stop();
        playing = false;
        AudioButton.GetComponentInChildren<TextMeshProUGUI>().text = "Audio: Aus";

    }

    public void ChangeAudio()
    {
        string temp;
        if (playing)
        {
            audioSource.Stop();
            playing = false;
            temp = "Aus";
        } else
        {
            audioSource.Play();
            playing = true;
            temp = "An";
        }
        AudioButton.GetComponentInChildren<TextMeshProUGUI>().text = "Audio: " + temp;
    }
}
