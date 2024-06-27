using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private HealthController healthController;
    void Update()
    {
        if (healthController == null)
        {
            healthController = FindObjectOfType<HealthController>();
        }
        if (healthController != null && healthController.RemainingHealthPercentage <= 0)
        {
            StartCoroutine(LoadMainMenu(3)); // laod Main Menu in 3 seconds

        }
    }

    IEnumerator LoadMainMenu(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        GameObject AudioManager = GameObject.FindWithTag("AudioManager");
        Destroy(AudioManager);
        SceneManager.LoadScene("MainMenu");
    }
}
