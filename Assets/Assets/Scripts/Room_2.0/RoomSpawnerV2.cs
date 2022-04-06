using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnerV2 : MonoBehaviour
{
    //room_To_Spawned corralate to room_Spawned
    [Header("Special Rooms")]
    public Transform StartRoom;
    public GameObject Exit_Room;
    [Header("Room Info")]
    public List<GameObject> rooms = new List<GameObject>();
    public List<GameObject> portal_Entrance = new List<GameObject>();

    int random_Value;
    public int room_To_Spawned;
    public int room_Cleared;
    public int room_Spawned;

    private void Start()
    {
        GenerateValue();
    }
    void GenerateValue()
    {
        if (room_Spawned < room_To_Spawned)
        {
            random_Value = Random.Range(0, rooms.Count - 1);
            SpawnRoom(random_Value);
        }
    }
    void SpawnRoom(int r)
    {
        rooms[r].SetActive(true);
        portal_Entrance.Add(rooms[r].transform.Find("EntrancePortal").gameObject.transform.Find("Gate_Entrance").gameObject);//get portal GO
        room_Spawned++;
        rooms.Remove(rooms[r]);
        if (room_Spawned == room_To_Spawned)
        {
            SpawnExitRoom();
        }
        else
        {
            GenerateValue();
        }

    }
    void SpawnExitRoom()
    {
        Exit_Room.SetActive(true);
        portal_Entrance.Add(Exit_Room.transform.Find("EntrancePortal").gameObject.transform.Find("Gate_Entrance").gameObject);
    }

}
