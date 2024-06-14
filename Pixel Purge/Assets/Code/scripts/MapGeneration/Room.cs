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

    private BoxCollider2D roomCollider;

    private List<Door> doors;

    private bool updatedDoors = false;
    public bool isBossRoom;

    public Room(int x, int y)
    {
        this.x = x;
        this.y = y;;
    }

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
            if (!door.ToString().Contains("Collider"))
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
        }

        roomCollider = GetComponent<BoxCollider2D>();

        RoomController.instance.RegisterRoom(this);
    }

    private void Update()
    {
        if (name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }

    public bool IsEnemyInsideRoom(Transform enemy)
    {
        return roomCollider.bounds.Contains(enemy.position);
    }

    public void UnlockDoors()
    {
        if (GetLeft() != null)
        {
            if (GetLeft() == RoomController.instance.loadedRooms[RoomController.instance.loadedRooms.Count - 1] && RoomController.instance.IfPlayerHasEnoughKeys())
            {
                UnlockLeftDoor();
            } else if (GetLeft() != RoomController.instance.loadedRooms[RoomController.instance.loadedRooms.Count - 1])
            {
                UnlockLeftDoor();
            }
        }
        if (GetRight() != null)
        {
            if (GetRight() == RoomController.instance.loadedRooms[RoomController.instance.loadedRooms.Count - 1] && RoomController.instance.IfPlayerHasEnoughKeys())
            {
                UnlockRightDoor();
            }
            else if (GetRight() != RoomController.instance.loadedRooms[RoomController.instance.loadedRooms.Count - 1])
            {
                UnlockRightDoor();
            }

            }
        if (GetTop() != null)
        {
            if (GetTop() == RoomController.instance.loadedRooms[RoomController.instance.loadedRooms.Count - 1] && RoomController.instance.IfPlayerHasEnoughKeys())
            {
                UnlockTopDoor();
            }
            else if (GetTop() != RoomController.instance.loadedRooms[RoomController.instance.loadedRooms.Count - 1])
            {
                UnlockTopDoor();
            }
        }
        if (GetBottom() != null)
        {
            if (GetBottom() == RoomController.instance.loadedRooms[RoomController.instance.loadedRooms.Count - 1] && RoomController.instance.IfPlayerHasEnoughKeys())
            {
                UnlockBottomDoor();
            }
            else if (GetBottom() != RoomController.instance.loadedRooms[RoomController.instance.loadedRooms.Count - 1])
            {
                UnlockBottomDoor();
            }
        }
    }

    private void UnlockLeftDoor()
    {
        colliderDoorLeft.gameObject.SetActive(false);
        GetLeft().colliderDoorRight.gameObject.SetActive(false);
    }

    private void UnlockRightDoor()
    {
        colliderDoorRight.gameObject.SetActive(false);
        GetRight().colliderDoorLeft.gameObject.SetActive(false);
    }

    private void UnlockTopDoor()
    {
        colliderDoorTop.gameObject.SetActive(false);
        GetTop().colliderDoorBottom.gameObject.SetActive(false);
    }

    private void UnlockBottomDoor()
    {
        colliderDoorBottom.gameObject.SetActive(false);
        GetBottom().colliderDoorTop.gameObject.SetActive(false);
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
                        RemoveLeftDoor(door);
                    }
                    break;
                case DoorType.right:
                    if (GetRight() == null)
                    {
                        RemoveRightDoor(door);
                    }
                    break;
                case DoorType.top:
                    if (GetTop() == null)
                    {
                        RemoveTopDoor(door);
                    }
                    break;
                case DoorType.bottom:
                    if (GetBottom() == null)
                    {
                        RemoveBottomDoor(door);
                    }
                    break;
            }

        }
    }

    private void RemoveLeftDoor(Door door)
    {
        colliderDoorLeft.gameObject.SetActive(true);
        door.gameObject.SetActive(false);
    }

    private void RemoveRightDoor(Door door)
    {
        colliderDoorRight.gameObject.SetActive(true);
        door.gameObject.SetActive(false);
    }

    private void RemoveTopDoor(Door door)
    {
        colliderDoorTop.gameObject.SetActive(true);
        door.gameObject.SetActive(false);
    }
    private void RemoveBottomDoor(Door door)
    {
        colliderDoorBottom.gameObject.SetActive(true);
        door.gameObject.SetActive(false);
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
