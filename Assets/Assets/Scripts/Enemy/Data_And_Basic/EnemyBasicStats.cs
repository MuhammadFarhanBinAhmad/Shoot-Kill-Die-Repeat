using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyBasicStats : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    internal GameObject text_Damage;
    [SerializeField] Image ui_Unit_Health, ui_Unit_Armour;

    [Header("SharedStats/Variables")]
    public EnemyBasicStatsSO EBSSO;
    public string unit_name;
    public float unit_Health,unit_Armour, unit_Damage;
    float fillAmount_Health, fillAmount_Armour;
    public bool destroy_Parent;
    public GameObject gameobject_Parent;
    [SerializeField]
    RoomInformation the_RM;
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

    private void Awake()
    {
        unit_name = (EBSSO.name);
        unit_Health = (EBSSO.health * FindObjectOfType<LevelManager>().stats_Multiplier[LevelManager.CURRENTLEVEL]);
        unit_Armour = (EBSSO.armour * FindObjectOfType<LevelManager>().stats_Multiplier[LevelManager.CURRENTLEVEL]);
        unit_Damage = (EBSSO.damage * FindObjectOfType<LevelManager>().stats_Multiplier[LevelManager.CURRENTLEVEL]);
        fillAmount_Armour = unit_Armour;
        fillAmount_Health = unit_Health;
        if (is_MovingEnemy)
        {
            unit_Speed = EBSSO.speed;
        }
        if (is_ProjectileEnemy)
        {
            unit_RoundType = EBSSO.round_Type;
            unit_FireRate = EBSSO.Fire_Rate;
        }
        if(GetComponentInParent<RoomInformation>() !=null)
        {
            the_RM = GetComponentInParent<RoomInformation>();
        }
        UpdateUI();
    }
    internal void TakingDamage(int dmg,GameObject GO)
    {
        if (the_BMEC != null)
        {
            the_BMEC.IsKnockBack();
        }
        if (unit_Armour >0)
        {
            unit_Armour -= dmg;
        }
        else
        {
            unit_Health -= dmg;
        }
        //Damage text facing player
        Vector3 direction = FindObjectOfType<PlayerManager>().transform.position - transform.position;
        Quaternion current_Rotation = Quaternion.LookRotation(-direction);
        //GameObject TD = Instantiate(text_Damage, GO.transform.position, current_Rotation);
        AmmoPool AP = FindObjectOfType<AmmoPool>();
        for (int i = 0; i < AP.text_Damage_Pool.Count; i++)
        {
            if (!AP.text_Damage_Pool[i].activeInHierarchy)
            {
                AP.text_Damage_Pool[i].transform.position = new Vector3(transform.position.x + (Random.Range(-.1f, .1f)), transform.position.y+ .55f, transform.position.z + (Random.Range(-.1f, .1f)));
                AP.text_Damage_Pool[i].transform.rotation = current_Rotation;
                AP.text_Damage_Pool[i].GetComponentInChildren<TextMeshProUGUI>().text = dmg.ToString();
                AP.text_Damage_Pool[i].SetActive(true);
                break;
            }
        }
        UpdateUI();

        if (unit_Health <= 0)
        {
            if(!destroy_Parent)
            {
                if (!dropped_Collectables)
                {
                    GetComponent<DropCollectables>().SpawnCollectables();
                    dropped_Collectables = true;
                }
                the_RM.enemy_In_Wave[the_RM.wave_Current].enemy.Remove(this.gameObject);
                the_RM.CheckTotalEnemy();
                Destroy(gameObject);
            }
            else
            {
                if (!dropped_Collectables)
                {
                    GetComponent<DropCollectables>().SpawnCollectables();
                    dropped_Collectables = true;
                }
                the_RM.enemy_In_Wave[the_RM.wave_Current].enemy.Remove(this.gameobject_Parent);
                the_RM.CheckTotalEnemy();
                Destroy(gameobject_Parent);
            }
        }
    }
    void UpdateUI()
    {
        ui_Unit_Armour.fillAmount = unit_Armour/fillAmount_Armour;
        ui_Unit_Health.fillAmount = unit_Health/fillAmount_Health;
    }
}
