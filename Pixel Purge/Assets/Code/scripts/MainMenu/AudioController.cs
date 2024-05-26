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

    private bool playing = false;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        audioSource.clip = audioClip;
        audioSource.Stop();
    }

    public void ChangeAudio()
    {
        String temp;
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
