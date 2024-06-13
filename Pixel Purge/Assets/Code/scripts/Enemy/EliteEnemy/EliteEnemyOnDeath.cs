using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EliteEnemyOnDeath : MonoBehaviour
{
    public GameObject eliteEnemy;

    private void OnDestroy()
    {
        if (eliteEnemy != null)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
