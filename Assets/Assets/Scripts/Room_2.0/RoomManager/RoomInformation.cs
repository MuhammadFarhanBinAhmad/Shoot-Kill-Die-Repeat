using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyWave
{
    public List<GameObject> enemy;
}

public class RoomInformation : MonoBehaviour
{

    //public List<EnemyBasicStats> enemy_Total = new List<EnemyBasicStats>();
    [Header("Index 0 = Wave 1")]
    [SerializeField]
    int wave_Total;
    [SerializeField]
    internal int wave_Current;
    public List<EnemyWave> enemy_In_Wave = new List<EnemyWave>();

    public GameObject _TeleBeam;
    public GameObject barrier_Exit;

    internal void SpawnWave()
    {
        for (int i = 0; i <= enemy_In_Wave[wave_Current].enemy.Count - 1; i++)
        {
            Instantiate(_TeleBeam, enemy_In_Wave[wave_Current].enemy[i].transform.position, Quaternion.identity);
            enemy_In_Wave[wave_Current].enemy[i].SetActive(true);
        }
    }
    public void CheckTotalEnemy()
    {
        if(enemy_In_Wave[wave_Current].enemy.Count == 0)
        {
            if (wave_Current == wave_Total)
            {
                barrier_Exit.SetActive(false);
                return;
            }
            wave_Current++;
            StartCoroutine("TimeBeforeSpawnWave");
        }
    }
    IEnumerator TimeBeforeSpawnWave()
    {
        yield return new WaitForSeconds(2);
        {
            SpawnWave();
        }
    }
}
