using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInformation : MonoBehaviour
{
    public List<EnemyBasicStats> enemy_Total = new List<EnemyBasicStats>();

    public GameObject barrier_Exit;
    public void CheckTotalEnemy()
    {
        if(enemy_Total.Count == 0)
        {
            barrier_Exit.SetActive(false);
            
        }
    }
}
