using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public List<GameObject> Data_NotToDestroyOnLoad;
    private void Start()
    {
        for (int i = 0; i <= Data_NotToDestroyOnLoad.Count -1; i++)
        {
            GameObject.DontDestroyOnLoad(Data_NotToDestroyOnLoad[i]);
            print("i");
        }
    }
}
