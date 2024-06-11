using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class RoomInfo
{
    public string name;
    public int x;
    public int y;
}

public class RoomController : MonoBehaviour
{

    public static RoomController instance;
    public List<Room> loadedRooms = new List<Room>();

    private string currentWorldName = "Basement";
    private Room currentRoom;
    private RoomInfo currentLoadRoomData;
    private Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    private BoxCollider2D RoomCollider;
    private bool isLoadingRoom = false;
    private bool spawnedBossRoom = false;
    private bool updatedRooms = false;
    private bool enemysRemaining = true;
    private bool unlocked = false;
    private int keyCount = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    private void Update()
    {
        UpdateRoomQueue();

        if (currentRoom != null)
        {
            if (unlocked)
            {
                UpdateEnemiesRemainingInCurrentRoom();
                if (!enemysRemaining)
                {
                    currentRoom.UnlockDoors();
                    unlocked = false;
                }
                if (IfPlayerHasEnoughKeys())
                {
                    Room bossRoom = GetBossRoom();
                    if (bossRoom != null)
                    {
                        bossRoom.UnlockDoors();
                    }
                }
            }
        }
    }
    public void SetKeyCount(int keyCount)
    {
        this.keyCount = keyCount;
    }

    public void InitUnlocking()
    {
        unlocked = true;
    }

    private Room GetBossRoom()
    {
        return loadedRooms[loadedRooms.Count - 1];
    }

    private void UpdateEnemiesRemainingInCurrentRoom()
    {
        bool enemiesRemaining = false;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (currentRoom.IsEnemyInsideRoom(enemy.transform))
            {
                enemiesRemaining = true;
                break;
            }
        }
        this.enemysRemaining = enemiesRemaining;
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }
        if(loadRoomQueue.Count == 0)
        {
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
                if(EntitySpawnController.instance != null)
                {
                    EntitySpawnController.instance.InitSpawning();
                }
            } else if (spawnedBossRoom && !updatedRooms)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                updatedRooms = true;
            }
            return;
        }
        
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
        
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if(loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.x, bossRoom.y);

            var roomToRemove = loadedRooms.Single(r => r.x == tempRoom.x && r.y == tempRoom.y);
            loadedRooms.Remove(roomToRemove);
            Destroy(bossRoom.gameObject);
            LoadRoom("End", tempRoom.x, tempRoom.y);
        }
    }

    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            Debug.Log($"Room at ({x}, {y}) already exists.");
            return;
        }

        RoomInfo newRoomData = new RoomInfo {
            name = name,
            x = x,
            y = y
        };

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = $"{currentWorldName}{info.name}";

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
        while (!loadRoom.isDone)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(currentLoadRoomData.x, currentLoadRoomData.y))
        {
            room.transform.position = new Vector3(
                currentLoadRoomData.x * room.width,
                currentLoadRoomData.y * room.height,
                0);

            room.x = currentLoadRoomData.x;
            room.y = currentLoadRoomData.y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.x + "," + room.y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currRoom = room;
            }

            loadedRooms.Add(room);
        } 
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }

    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => (item.x == x && item.y == y));
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => (item.x == x && item.y == y)) != null;
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currentRoom = room;
        unlocked = true;
    }

    public Boolean IfPlayerHasEnoughKeys()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && player.GetComponent<KeyController>().keyCount >= keyCount)
        {
            return true;
        }
        return false;
    }
}
