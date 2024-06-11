using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EntitySpawnController : MonoBehaviour
{
    public static EntitySpawnController instance;
    private List<Room> loadedRooms;

    public int minNumEnemy;
    public int maxNumEnemy;
    public GameObject enemyPrefab;

    public int minNumKey;
    public int maxNumKey;
    public GameObject keyPrefab;


    private void Awake()
    {
        instance = this;
    }

    public void InitSpawning()
    {
        if (RoomController.instance != null)
        {
            this.loadedRooms = RoomController.instance.loadedRooms;

            if (loadedRooms != null && loadedRooms.Count > 0)
            {
                loadedRooms[loadedRooms.Count - 1].isBossRoom = true;
                SpawnEntitiesInRooms(minNumEnemy, maxNumEnemy, enemyPrefab);
                SpawnKeysForBossRoom(minNumKey, maxNumKey, keyPrefab);
                RoomController.instance.InitUnlocking();
            }
            else
            {
                Debug.LogError("No rooms loaded or loadedRooms is null.");
            }
        }
    }

    private void SpawnEntitiesInRooms(int minNum, int maxNum, GameObject prefab)
    {
        int roomCount = loadedRooms.Count;
        for (int i = 0; i < roomCount; i++)
        {
            Room room = loadedRooms[i];
            if (room != null && !room.name.Contains("Start") && !room.isBossRoom)
            {
                BoxCollider2D roomCollider = room.GetComponent<BoxCollider2D>();
                if (roomCollider == null)
                {
                    continue;
                }

                int numEntitiesToSpawn = (int) Mathf.Lerp(minNum, maxNum, (float)i / (roomCount - 1));

                for (int j = 0; j < numEntitiesToSpawn; j++)
                {
                    if (prefab == null)
                    {
                        Debug.Log("Prefab is null, can't load prefab!");
                    }
                    else
                    {
                        Vector3 spawnPosition = GetRandomPositionInRoom(roomCollider, prefab);
                        Instantiate(prefab, spawnPosition, Quaternion.identity);
                    }
                }
            }
        }
    }

    private void SpawnKeysForBossRoom(int minNum, int maxNum, GameObject prefab)
    {
        int numKeys = Random.Range(minNum, maxNum);
        RoomController.instance.SetKeyCount(numKeys);
        List<Room> rooms = new List<Room>(loadedRooms);

        int keysSpawned = 0;
        for (int i = 0; i < numKeys; i++)
        {
            BoxCollider2D roomCollider = rooms[(int) Random.Range(1, loadedRooms.Count - 2)].GetComponent<BoxCollider2D>();
            if (roomCollider != null)
            {
                Vector3 spawnPosition = GetRandomPositionInRoom(roomCollider, prefab);
                Instantiate(prefab, spawnPosition, Quaternion.identity);
                keysSpawned++;
            }
        }
        
    }

    private Vector3 GetRandomPositionInRoom(BoxCollider2D roomCollider, GameObject prefab)
    {
        Bounds bounds = roomCollider.bounds;

        float x = Random.Range(bounds.min.x + prefab.transform.localScale.x / 2, bounds.max.x - prefab.transform.localScale.x / 2);
        float y = Random.Range(bounds.min.y + prefab.transform.localScale.y / 2, bounds.max.y - prefab.transform.localScale.y / 2);

        return new Vector3(x, y, 0);
    }
}
