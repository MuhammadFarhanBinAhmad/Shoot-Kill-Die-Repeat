using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBasicStats : MonoBehaviour
{
    [Header("RoundType")]
    public EnemyBasicStatsSO EBSSO;
    public int unit_Speed, unit_Health, unit_Damage,unit_RoundType,unit_FireRate;

    [SerializeField]
    NavMeshAgent agent;

    public bool destroy_Parent;
    internal bool stats_Multipled;
    bool dropped_Collectables;

    private void Awake()
    {
        unit_Speed = EBSSO.speed;
        unit_Health = EBSSO.health;
        unit_Damage = EBSSO.damage;
        unit_RoundType = EBSSO.round_Type;
        unit_FireRate = EBSSO.Fire_Rate;
    }

    internal void TakingDamage(int dmg)
    {
        unit_Health -= dmg;
        if (unit_Health <= 0)
        {
            if(!destroy_Parent)
            {
                if (!dropped_Collectables)
                {
                    GetComponent<DropCollectables>().SpawnCollectables();
                    dropped_Collectables = true;
                }
                Destroy(gameObject);
            }
            else
            {
                if (!dropped_Collectables)
                {
                    GetComponent<DropCollectables>().SpawnCollectables();
                    dropped_Collectables = true;
                }
                Destroy(transform.parent.gameObject);
            }
        }
    }
    internal void MultipleStats(int buff_Multiplier)
    {
        unit_Speed *= buff_Multiplier;
        agent.speed = unit_Speed;
        unit_Health *= buff_Multiplier;
        unit_Damage *= buff_Multiplier;
        stats_Multipled = true;
        StartCoroutine(ResetStats(buff_Multiplier));
    }
    internal IEnumerator ResetStats(int multiplier)
    {
        yield return new WaitForSeconds(5);
        if (stats_Multipled)
        {
            unit_Speed /= multiplier;
            agent.speed = unit_Speed;
            unit_Health /= multiplier;
            unit_Damage /= multiplier;
            stats_Multipled = false;
        }
    }
}
