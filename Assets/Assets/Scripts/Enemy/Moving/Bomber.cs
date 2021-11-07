using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    public GameObject explosive_Mine;

    public float spawn_Time;
    public Transform spawn_Point;

    private void Start()
    {
        InvokeRepeating("SpawnMine", 2, spawn_Time);
    }

    void SpawnMine()
    {
        GameObject M = Instantiate(explosive_Mine, spawn_Point.position, spawn_Point.rotation);
    }
}
