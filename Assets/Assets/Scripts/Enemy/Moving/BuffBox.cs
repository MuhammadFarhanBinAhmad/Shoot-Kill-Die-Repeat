using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBox : MonoBehaviour
{
    public List<EnemyBasicStats> buff_Enemy = new List<EnemyBasicStats>();

    public int buff_Multiplier;

    void BuffEnemyStats(EnemyBasicStats BE)
    {
        if (!BE.stats_Multipled)
        {
            BE.MultipleStats(buff_Multiplier);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyBasicStats>() !=null)
        {
            buff_Enemy.Add(other.GetComponent<EnemyBasicStats>());
            BuffEnemyStats(other.GetComponent<EnemyBasicStats>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyBasicStats>() != null)
        {
            other.GetComponent<EnemyBasicStats>().StartCoroutine(other.GetComponent<EnemyBasicStats>().ResetStats(buff_Multiplier));
            buff_Enemy.Remove(other.GetComponent<EnemyBasicStats>());
        }
    }

}
