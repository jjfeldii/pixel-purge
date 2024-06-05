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


    //TODO: GameObject Key, Gen Key, Unlock BossRoom if all keys collected
    private void SpawnKeysForBossRoom(int minNum, int maxNum, GameObject prefab)
    {
        int numKeys = Random.Range(minNum, maxNum);

        List<Room> rooms = new List<Room>(loadedRooms);

        // Sortiere die Räume nach Entfernung vom Ursprungspunkt (0,0)
        rooms.Sort((a, b) => Vector2.Distance(Vector2.zero, new Vector2(b.x, b.y)).CompareTo(Vector2.Distance(Vector2.zero, new Vector2(a.x, a.y))));

        // Spawne die Schlüssel in den weitest entfernten Räumen, außer im Boss-Raum
        int keysSpawned = 0;
        foreach (Room room in rooms)
        {
            if (keysSpawned >= numKeys)
                break;

            if (room.isBossRoom)
                continue;

            BoxCollider2D roomCollider = room.GetComponent<BoxCollider2D>();
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
