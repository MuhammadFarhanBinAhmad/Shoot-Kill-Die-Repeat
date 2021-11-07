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
            /*BE.unit_Speed *= buff_Multiplier;
            BE.unit_Health *= buff_Multiplier;
            BE.unit_Damage *= buff_Multiplier;
            BE.stats_Multipled = true;
            BE.StartCoroutine(BE.ResetStats(buff_Multiplier));*/
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
