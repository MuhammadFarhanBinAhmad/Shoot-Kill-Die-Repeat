using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Room : MonoBehaviour
{
    RoomSpawner the_RS;
    //public NavMeshSurface the_NMS;

    public bool room_Completed;

    /*public List<GameObject> vending_Machine = new List<GameObject>();
    public Transform vending_Machine_Spawn_Point;*/


    public List<GameObject> enemy_Left = new List<GameObject>();
    private void Start()
    {
        int r = Random.Range(0,10);

        /*if (vending_Machine[r] != null)
        {
            Instantiate(vending_Machine[r], vending_Machine_Spawn_Point.position, vending_Machine_Spawn_Point.rotation);
            print("VM spawn");
        }
        else
        {
            print("Nothing Spawn");
        }*/

        foreach(GameObject GO in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (GO.tag == "Enemy")
            {
                enemy_Left.Add(GO);
            }
        }
    }
    public void CheckEnemy()
    {
        if (enemy_Left.Count == 0)
        {
            room_Completed = true;
        }
        else
        {
            print(enemy_Left.Count);
        }
    }


}
