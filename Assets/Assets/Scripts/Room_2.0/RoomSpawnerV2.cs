using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnerV2 : MonoBehaviour
{
    //room_To_Spawned corralate to room_Spawned
    //element 0 = level 1
    [Header("Special Rooms")]
    public Transform SpawnRoom;
    public GameObject Exit_Room;
    [Header("Room Info")]
    public List<int> room_To_Spawned = new List<int>();
    public List<Transform> room_Pos = new List<Transform>();
    public List<GameObject> rooms = new List<GameObject>();
    [Header("Ensure no.of bool = no. of rooms")]
    public List<bool> room_Already_Spawned_Bool = new List<bool>();
    public List<GameObject> room_Already_Spawned_GO = new List<GameObject>();

    public List<GameObject> portal_Entrance = new List<GameObject>();

    public int room_Cleared;
    public int current_LVL;
    int room_Spawned;

    private void Start()
    {
        //portal_Exit.Add(SpawnRoom.Find("ExitPortal(NextRoom)").gameObject);
        //room_Already_Spawned_GO.Add(SpawnRoom.gameObject);
        GenerateValue();
    }
    void GenerateValue()
    {
        for (int i = 0; i < room_To_Spawned[current_LVL]; i++)
        {
            int rn = Random.Range(0, rooms.Count);
            if (room_Already_Spawned_Bool[rn])
            {
                GenerateValue();
                break;
            }
            else
            {
                GameObject R = Instantiate(rooms[rn], room_Pos[room_Spawned].position, room_Pos[room_Spawned].rotation);//Spawn Room
                room_Already_Spawned_Bool[rn] = true;//Check this room has already spawn
                room_Already_Spawned_GO.Add(R);
                portal_Entrance.Add(R.transform.Find("EntrancePortal").gameObject.transform.Find("Gate_Entrance").gameObject);//get portal GO
                room_Spawned++;
            }
        }
        SpawnExitRoom();
    }
    void SpawnExitRoom()
    {
        GameObject EXR = Instantiate(Exit_Room, room_Pos[room_Pos.Count-1].position, room_Pos[room_Pos.Count - 1].rotation);
        portal_Entrance.Add(EXR.transform.Find("EntrancePortal").gameObject.transform.Find("Gate_Entrance").gameObject);
    }

}
