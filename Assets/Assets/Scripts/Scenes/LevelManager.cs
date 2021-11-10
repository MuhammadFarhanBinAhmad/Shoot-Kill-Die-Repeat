using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int CURRENTLEVEL = 0;
    public List<float> stats_Multiplier = new List<float>();
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
