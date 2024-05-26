using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public Transform player;
    private Vector2 boundary = new Vector2(9, 5);
    private const float CAMERA_SPEED = 0.125f;
    private Vector2 cameraShift = new Vector2(0,0);


    void LateUpdate()
    {
        if (player != null)
        {
            // Berechne die aktuelle Position des Spielers im Viewport
            Vector3 cameraPos = GameObject.FindWithTag("MainCamera").transform.position;

            if(player.position.x < cameraPos.x - boundary.x)
            {
                cameraPos.x -= 16 * boundary.x;
            } else if(player.position.x > cameraPos.x + boundary.x)
            {
                cameraPos.x += 16 * boundary.x;
            }
            

            if (player.position.y < cameraPos.y - boundary.y)
            {
                cameraPos.y -= 16 * boundary.y;
            }
            else if (player.position.y > cameraPos.y + boundary.y)
            {
                cameraPos.y += 16 * boundary.y;
            }

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, cameraPos, CAMERA_SPEED);
            transform.position = smoothedPosition;

        }
    }
}
