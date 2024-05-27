using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;
    public Room currRoom;
    public float moveSpeedWhenRoomChange;

    /*
    public Transform player;

    private Vector2 boundary = new Vector2(9, 5);
    private const float CAMERA_SPEED = 0.125f;

    void FixedUpdate()
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

            transform.position = Vector3.Lerp(transform.position, cameraPos, CAMERA_SPEED);

        }
    }
     */

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (currRoom == null)
        {
            return;
        }

        Vector3 targetPos = GetCameraTargetPosition();
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeedWhenRoomChange);
    }

    private Vector3 GetCameraTargetPosition()
    {
        if(currRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPos = currRoom.GetRoomCenter();
        targetPos.z = transform.position.z;
        return targetPos;
    }

    public bool IsSwitchingScene()
    {
        return !transform.position.Equals(GetCameraTargetPosition());
    }
}
