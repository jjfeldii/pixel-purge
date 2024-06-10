using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;

    private void Start()
    {
        if (PlayerPrefs.HasKey("SelectedColor"))
        {
            string colorHex = PlayerPrefs.GetString("SelectedColor");
            var r = colorHex.Substring(0, 2);
            var g = colorHex.Substring(2, 2);
            var b = colorHex.Substring(4, 2);
            string alpha;
            if (colorHex.Length >= 8)
            {
                alpha = colorHex.Substring(6, 2);
            }
            else
            {
                alpha = "FF";
            }

            CreatePlayer (new Color((int.Parse(r, NumberStyles.HexNumber) / 255f),
                            (int.Parse(g, NumberStyles.HexNumber) / 255f),
                            (int.Parse(b, NumberStyles.HexNumber) / 255f),
                            (int.Parse(alpha, NumberStyles.HexNumber) / 255f)));
        }
        else
        {
            Debug.LogWarning("No color selected! Using default color.");
            CreatePlayer(Color.white);
        }
    }

    private void CreatePlayer(Color color)
    {
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player.name = "Player";

        GameObject playerGraphics = GameObject.Find("Graphics");
        playerGraphics.GetComponent<SpriteRenderer>().color = color;
    }
}