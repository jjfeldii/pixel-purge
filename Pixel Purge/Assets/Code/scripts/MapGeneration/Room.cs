using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int width;
    public int height;
    public int x;
    public int y;

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public Door colliderDoorLeft;
    public Door colliderDoorRight;
    public Door colliderDoorTop;
    public Door colliderDoorBottom;

    List<Door> doors;

    private bool updatedDoors = false;

    public Room(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (RoomController.instance == null)
        {
            return;
        }

        doors = new List<Door>();

        Door[] ds = GetComponentsInChildren<Door>();
        foreach (Door door in ds)
        {
            doors.Add(door);
            switch (door.doorType)
            {
                case DoorType.left:
                    leftDoor = door;
                    break;
                case DoorType.right:
                    rightDoor = door;
                    break;
                case DoorType.top:
                    topDoor = door;
                    break;
                case DoorType.bottom:
                    bottomDoor = door;
                    break;
            }
        }

        RoomController.instance.RegisterRoom(this);
    }

    private void Update()
    {
        if(name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case DoorType.left:
                    if (GetLeft() == null)
                    {
                        colliderDoorLeft.gameObject.SetActive(true);
                        door.gameObject.SetActive(false);
                    }
                    break;
                case DoorType.right:
                    if (GetRight() == null)
                    {
                        colliderDoorRight.gameObject.SetActive(true);
                        door.gameObject.SetActive(false);
                    }
                    break;
                case DoorType.top:
                    if (GetTop() == null)
                    {
                        colliderDoorTop.gameObject.SetActive(true);
                        door.gameObject.SetActive(false);
                    }
                    break;
                case DoorType.bottom:
                    if (GetBottom() == null)
                    {
                        colliderDoorBottom.gameObject.SetActive(true);
                        door.gameObject.SetActive(false);
                    }
                    break;
            }

        }
    }




    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(x - 1, y))
        {
            return RoomController.instance.FindRoom(x - 1, y);
        }
        return null;
    }

    public Room GetRight()
    {
        if (RoomController.instance.DoesRoomExist(x + 1, y))
        {
            return RoomController.instance.FindRoom(x + 1, y);
        }
        return null;
    }

    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExist(x, y + 1))
        {
            return RoomController.instance.FindRoom(x, y + 1);
        }
        return null;
    }

    public Room GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(x, y - 1))
        {
            return RoomController.instance.FindRoom(x, y - 1);
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3 (x * width, y * height );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
