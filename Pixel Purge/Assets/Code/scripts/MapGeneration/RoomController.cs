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

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //TODO Load not Working
        LoadRoom("Start", 0, 0);
        LoadRoom("Empty", 1, 0);
        LoadRoom("Empty", -1, 0);
        LoadRoom("Empty", 0, 1);
        LoadRoom("Empty", 0, -1);

    }


    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if(loadRoomQueue.Count == 0)
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
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.x = x;
        newRoomData.y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
        while (loadRoom.isDone == false)
        {
            yield return null;
        }

        isLoadingRoom = false;
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

        isLoadingRoom = false;
        loadedRooms.Add(room);
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.x == x && item.y == y) != null;
    }
}
