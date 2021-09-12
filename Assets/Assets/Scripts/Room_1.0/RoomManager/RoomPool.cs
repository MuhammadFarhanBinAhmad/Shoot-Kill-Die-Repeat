using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPool : MonoBehaviour
{

    public List<GameObject> Rooms = new List<GameObject>();
    internal List<GameObject> room_Pool = new List<GameObject>();

    void Start()
    {
        for (int r = 0; r < Rooms.Count; r++)
        {
            GameObject R = Instantiate(Rooms[r]);
            room_Pool.Add(R);
            R.SetActive(false);
        }
    }
    
}
