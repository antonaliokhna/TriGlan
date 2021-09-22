using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomsPlacer : MonoBehaviour
{
    public GameObject NextLvlObjectPrefub;
    public GameObject BossRoomPrefub;
    public GameObject SpawnedBossRoom;

    private GameObject spawnLvlObject;

    public Room[] RoomPrefabs;
    public Room StartingRoom;

    private Room[,] spawnedRooms;

    public GameObject MapPrefabs;
    private GameObject[,] SpawnedMapEl;

    static System.Random r = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        StartSpawnRooms();
    }

    public void DestroyBossRoom()
    {
        if (SpawnedBossRoom != null)
        {
            SpawnedBossRoom.GetComponent<RoomBoss>().DestroyRoomObjects();
            Destroy(this.SpawnedBossRoom.gameObject);
        }
    }

    public void SpawnBossRoom()
    {
        SpawnedBossRoom = Instantiate(BossRoomPrefub, new Vector3(0, 0, 1), this.transform.rotation);
    }

    public void StartSpawnRooms()
    {
        SpawnedMapEl = new GameObject[4, 4];

        int countEteration = 16;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                SpawnedMapEl[i, j] = MapPrefabs.transform.Find($"Map{countEteration}").gameObject;
                SpawnedMapEl[i, j].SetActive(false);
                countEteration--;
            }
        }


        spawnedRooms = new Room[4, 4];
        SpawnedMapEl[2, 2].SetActive(true);
        spawnedRooms[2, 2] = Instantiate(StartingRoom, new Vector3(0, 0, 1), transform.rotation);

        for (int i = 0; i < 8; i++)
        {
            PlaceOneRoom();
        }

        SpawnNextLvl();
    }

    private void SpawnNextLvl()
    {
        float xNextLvl = 0;
        float yNextLvl = 0;

        for (int i = 0; i < spawnedRooms.GetLength(0); i++)
        {
            for (int j = 0; j < spawnedRooms.GetLength(1); j++)
            {
                if (spawnedRooms?[i, j] != null)
                {
                    if (Math.Abs(spawnedRooms[i, j].transform.position.x) > Math.Abs(xNextLvl) && Math.Abs(spawnedRooms[i, j].transform.position.y) > Math.Abs(yNextLvl))
                    {
                        xNextLvl = spawnedRooms[i, j].transform.position.x;
                        yNextLvl = spawnedRooms[i, j].transform.position.y;
                    }
                }
            }
        }

        this.spawnLvlObject = Instantiate(NextLvlObjectPrefub, new Vector3((float)xNextLvl, (float)yNextLvl, 1), transform.rotation);
    }

    public void DestroyRooms()
    {
        for (int i = 0; i < spawnedRooms.GetLength(0); i++)
        {
            for (int j = 0; j < spawnedRooms.GetLength(1); j++)
            {
                if (spawnedRooms?[i, j] != null)
                {
                    for (int k = 0; k < spawnedRooms[i, j].spawnedBox.GetLength(0); k++)
                    {
                        Destroy(spawnedRooms[i, j].spawnedBox[k].gameObject);
                    }
                    if (spawnedRooms[i, j]?.useChest != null)
                    {
                        Destroy(spawnedRooms[i, j].useChest.gameObject);
                    }
                    spawnedRooms[i, j].DestroyTrictangles();
                    spawnedRooms[i, j].DestroyWalls();
                    Destroy(spawnedRooms[i, j].gameObject);
                }
            }
            Destroy(spawnLvlObject.gameObject);
        }
    }

    // Update is called once per frame
    void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }
        Room newRoom = Instantiate(RoomPrefabs[UnityEngine.Random.Range(0, RoomPrefabs.Length)]);


        int limit = 500;
        while (limit-- > 0)
        {
            // Эту строчку можно заменить на выбор положения комнаты с учётом того насколько он далеко/близко от центра,
            Vector2Int position = vacantPlaces.ElementAt(UnityEngine.Random.Range(0, vacantPlaces.Count));
            if (ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x - 2, position.y - 2, 0) * 215;
                SpawnedMapEl[position.x, position.y].SetActive(true);
                spawnedRooms[position.x, position.y] = newRoom;
                break;
            }
        }
    }
    private bool ConnectToSomething(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorT != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorB != null) neighbours.Add(Vector2Int.up);
        if (room.DoorB != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorT != null) neighbours.Add(Vector2Int.down);
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorL != null) neighbours.Add(Vector2Int.right);
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorR != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[UnityEngine.Random.Range(0, neighbours.Count)];
        Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        if (selectedDirection == Vector2Int.up)
        {
            room.DoorT.SetActive(false);
            selectedRoom.DoorB.SetActive(false);
            for (int i = 0; i < 2; i++)
            {
                room.DoorTPass[i].SetActive(true);
                selectedRoom.DoorBPass[i].SetActive(true);
            }
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.DoorB.SetActive(false);
            selectedRoom.DoorT.SetActive(false);
            for (int i = 0; i < 2; i++)
            {
                room.DoorBPass[i].SetActive(true);
                selectedRoom.DoorTPass[i].SetActive(true);
            }
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.DoorR.SetActive(false);
            selectedRoom.DoorL.SetActive(false);
            for (int i = 0; i < 2; i++)
            {
                room.DoorRPass[i].SetActive(true);
                selectedRoom.DoorLPass[i].SetActive(true);
            }
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.DoorL.SetActive(false);
            selectedRoom.DoorR.SetActive(false);
            for (int i = 0; i < 2; i++)
            {
                room.DoorLPass[i].SetActive(true);
                selectedRoom.DoorRPass[i].SetActive(true);
            }
        }

        return true;
    }
}
