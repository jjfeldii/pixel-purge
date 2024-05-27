using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private RoomInfo currentLoadRoomData;
    private Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    private bool isLoadingRoom = false;
    private Room currentRoom;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //TODO: Implement MapGen Algorithm

        LoadRoom("Start", 0, 0);
        LoadRoom("Empty", 1, 0);
        LoadRoom("Empty", 0, 1);
        LoadRoom("Empty", 0, -1);
        LoadRoom("Empty", -1, 0);

    }


    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom || loadRoomQueue.Count == 0)
        {
            return;
        }
        
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
        
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

        Debug.Log($"Enqueuing room: {name} at ({x}, {y})");
        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = $"{currentWorldName}{info.name}";
        Debug.Log($"Loading room: {roomName}");

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
        while (!loadRoom.isDone)
        {
            yield return null;
        }
        Debug.Log($"Room loaded: {roomName}");
        
    }

    public void RegisterRoom(Room room)
    {
        room.transform.position = new Vector3(
            currentLoadRoomData.x * room.width,
            currentLoadRoomData.y * room.height,
            0);

        room.x = currentLoadRoomData.x;
        room.y = currentLoadRoomData.y;
        room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.x + "," + room.y;
        room.transform.parent = transform;


        if(loadedRooms.Count == 0)
        {
            CameraController.instance.currRoom = room;
        }

        loadedRooms.Add(room);
        isLoadingRoom = false;
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => (item.x == x && item.y == y)) != null;
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currentRoom = room;
    }
}
