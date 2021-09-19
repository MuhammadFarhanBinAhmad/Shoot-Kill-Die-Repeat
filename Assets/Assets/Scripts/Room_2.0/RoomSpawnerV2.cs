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
    public int room_To_Spawned;
    public List<Transform> room_Pos = new List<Transform>();
    public List<GameObject> rooms = new List<GameObject>();
    [Header("Ensure no.of bool = no. of rooms")]
    public List<bool> room_Already_Spawned_Bool = new List<bool>();
    public List<GameObject> room_Already_Spawned_GO = new List<GameObject>();
    public List<GameObject> portal_Entrance = new List<GameObject>();

    public static int current_Level;
    public int room_Cleared;
    int room_Spawned;

    bool Exit_Room_Spawned;

    private void Start()
    {
        GenerateValue();
    }
    void GenerateValue()
    {
        for (int i = 0; i < room_To_Spawned; i++)
        {
            int rn = Random.Range(0, rooms.Count);
            if (room_Already_Spawned_Bool[rn])
            {
                GenerateValue();
                break;
            }
            else
            {
                SpawnRoom(rn);
            }
        }
    }
    void SpawnRoom(int r)
    {
        GameObject R = Instantiate(rooms[r], room_Pos[room_Spawned].position, room_Pos[room_Spawned].rotation);//Spawn Room
        room_Already_Spawned_Bool[r] = true;//Check this room has already spawn
        room_Already_Spawned_GO.Add(R);
        portal_Entrance.Add(R.transform.Find("EntrancePortal").gameObject.transform.Find("Gate_Entrance").gameObject);//get portal GO
        room_Spawned++;
        if (room_Spawned == room_To_Spawned)
        {
            SpawnExitRoom();
        }
    }
    void SpawnExitRoom()
    {
        if (!Exit_Room_Spawned)
        {
            Exit_Room_Spawned = true;
            GameObject EXR = Instantiate(Exit_Room, room_Pos[room_Pos.Count - 1].position, room_Pos[room_Pos.Count - 1].rotation);
            portal_Entrance.Add(EXR.transform.Find("EntrancePortal").gameObject.transform.Find("Gate_Entrance").gameObject);
        }
    }

}
