using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLauncher : MonoBehaviour
{
    public GameObject Misslelauncher;
    public GameObject Flamethrower;


    internal void SpawnMissleLauncher()
    {
        Misslelauncher.SetActive(true);
    }
    internal void SpawnFlameThrower()
    {
        Flamethrower.SetActive(true);
    }
}
