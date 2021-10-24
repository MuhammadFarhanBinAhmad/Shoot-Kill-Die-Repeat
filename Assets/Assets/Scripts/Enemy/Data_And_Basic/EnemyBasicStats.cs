using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicStats : MonoBehaviour
{
    [Header("RoundType")]
    public EnemyBasicStatsSO EBSSO;
    public int unit_Speed, unit_Health, unit_Damage,unit_RoundType,unit_FireRate;

    public bool destroy_Parent;

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
}
