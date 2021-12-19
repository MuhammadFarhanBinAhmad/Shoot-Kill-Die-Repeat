using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int CURRENTLEVEL = 0;
    public static bool[] weapon_Unlocked = new bool[6];

    public List<float> stats_Multiplier = new List<float>();
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        weapon_Unlocked[0] = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("TestRoom");
        }
    }
}
