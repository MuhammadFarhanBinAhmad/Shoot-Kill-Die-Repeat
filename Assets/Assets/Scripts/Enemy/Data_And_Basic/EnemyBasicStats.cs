using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyBasicStats : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    internal GameObject text_Damage;

    [Header("SharedStats/Variables")]
    public EnemyBasicStatsSO EBSSO;
    public float unit_Health, unit_Damage;
    public bool destroy_Parent;
    public GameObject gameobject_Parent;

    internal bool stats_Multipled;
    bool dropped_Collectables;

    [Header("ForEnemyShootingProjectile")]
    [SerializeField]
    bool is_ProjectileEnemy;
    public float unit_RoundType,unit_FireRate;

    [Header("ForMovingEnemy")]
    [SerializeField]
    bool is_MovingEnemy;
    public float unit_Speed;
    public BasicMovingEnemyComponents the_BMEC;
    public RandomMovementEnemy the_RME;
    [SerializeField]
    NavMeshAgent agent;

    [Header("ForStaticEnemy")]
    [SerializeField]
    bool is_StaticEnemy;

    private void Awake()
    {
        unit_Health = (EBSSO.health * FindObjectOfType<LevelManager>().stats_Multiplier[LevelManager.CURRENTLEVEL]);
        unit_Damage = (EBSSO.damage * FindObjectOfType<LevelManager>().stats_Multiplier[LevelManager.CURRENTLEVEL]);
        if (is_MovingEnemy)
        {
            agent = GetComponent<NavMeshAgent>();
            unit_Speed = EBSSO.speed;
        }
        if (is_ProjectileEnemy)
        {
            unit_RoundType = EBSSO.round_Type;
            unit_FireRate = EBSSO.Fire_Rate;
        }
        if (is_StaticEnemy)
        {

        }
    }

    internal void TakingDamage(int dmg,GameObject GO)
    {
        if (the_BMEC != null)
        {
            the_BMEC.IsKnockBack();
        }

        unit_Health -= dmg;

        Vector3 direction = FindObjectOfType<PlayerManager>().transform.position - transform.position;
        Quaternion current_Rotation = Quaternion.LookRotation(-direction);
        //GameObject TD = Instantiate(text_Damage, GO.transform.position, current_Rotation);
        AmmoPool AP = FindObjectOfType<AmmoPool>();
        for (int i = 0; i < AP.spark_Pool.Count; i++)
        {
            if (!AP.text_Damage_Pool[i].activeInHierarchy)
            {
                AP.text_Damage_Pool[i].transform.position = new Vector3(transform.position.x + (Random.Range(-.1f, .1f)), transform.position.y, transform.position.z + (Random.Range(-.1f, .1f)));
                AP.text_Damage_Pool[i].transform.rotation = current_Rotation;
                AP.text_Damage_Pool[i].GetComponentInChildren<TextMeshProUGUI>().text = dmg.ToString();
                AP.text_Damage_Pool[i].SetActive(true);
                break;
            }
        }

        if (unit_Health <= 0)
        {
            if(!destroy_Parent)
            {
                if (!dropped_Collectables)
                {
                    GetComponent<DropCollectables>().SpawnCollectables();
                    dropped_Collectables = true;
                }
                //FindObjectOfType<RoomInformation>().enemy_Total.Remove(this);
                //FindObjectOfType<RoomInformation>().CheckTotalEnemy();
                Destroy(gameObject);
            }
            else
            {
                if (!dropped_Collectables)
                {
                    GetComponent<DropCollectables>().SpawnCollectables();
                    dropped_Collectables = true;
                }
                //FindObjectOfType<RoomInformation>().enemy_Total.Remove(this);
                //FindObjectOfType<RoomInformation>().CheckTotalEnemy();
                Destroy(gameobject_Parent);
            }
        }
    }

    internal void MultipleStats(int buff_Multiplier)
    {
        if (is_MovingEnemy)
        {
            unit_Speed *= buff_Multiplier;
            agent.speed = unit_Speed;
        }
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
            if (is_MovingEnemy)
            {
                unit_Speed /= multiplier;
                agent.speed = unit_Speed;
            }
            unit_Health /= multiplier;
            unit_Damage /= multiplier;
            stats_Multipled = false;
        }
    }
}
