using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInput_HurtEnemy : MonoBehaviour
{
    [SerializeField]
    internal EnemyBasicStats the_EBS;

    public bool is_Explosive_Universal, is_Explosive_Normal,is_Taking_Fire_Damage;

    private void Start()
    {
        the_EBS = GetComponent<EnemyBasicStats>();
    }

    internal void TakeBulletDamage(BulletStats_ForPlayer BSFP)
    {
        int new_Bullet_Damage;
        //Damage value drop over time
        float P = (int)Mathf.Round((BSFP.bullet_Active_Up_Time / BSFP.bullet_Active_Time) * 100);//percent
        new_Bullet_Damage = (int)Mathf.Round((P / 100) * BSFP.bullet_Damage);
        the_EBS.TakingDamage(new_Bullet_Damage, this.gameObject);

        BSFP.Destroy();

        if (the_EBS.unit_Health <= 0)
        {
            EnemyDeath();
        }
    }
    internal void TakeFireDamage(int FD)
    {
        the_EBS.TakingDamage(FD,this.gameObject);
        if (the_EBS.unit_Health <= 0)
        {
            EnemyDeath();
        }
    }
    internal void TakeExplosionDamage(int DMG)
    {
        the_EBS.TakingDamage(DMG, this.gameObject);
        if (the_EBS.unit_Health <= 0)
        {
            EnemyDeath();
        }
    }
    void ExplosiveUniversal()
    {
        AmmoPool AP = FindObjectOfType<AmmoPool>();
        for (int i = 0; i < AP.Explosion_Universal_Pool.Count; i++)
        {
            if (!AP.Explosion_Universal_Pool[i].activeInHierarchy)
            {
                AP.Explosion_Universal_Pool[i].transform.position = this.transform.position;
                AP.Explosion_Universal_Pool[i].transform.rotation = this.transform.rotation;
                AP.Explosion_Universal_Pool[i].SetActive(true);
                break;
            }
        }
    }
    void ExplosiveNormal()
    {
        AmmoPool AP = FindObjectOfType<AmmoPool>();
        for (int i = 0; i < AP.Explosion_Normal_Pool.Count; i++)
        {
            if (!AP.Explosion_Normal_Pool[i].activeInHierarchy)
            {
                AP.Explosion_Normal_Pool[i].transform.position = this.transform.position;
                AP.Explosion_Normal_Pool[i].transform.rotation = this.transform.rotation;
                AP.Explosion_Normal_Pool[i].SetActive(true);
                break;
            }
        }
    }
    void EnemyDeath()
    {
        if (is_Explosive_Universal)
        {
            ExplosiveUniversal();
        }
        if (is_Explosive_Normal)
        {
            ExplosiveNormal();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletStats_ForPlayer>() != null && other.GetComponent<BulletStats_ForPlayer>().tag == "HurtEnemy")
        {
            BulletStats_ForPlayer BSFP = other.GetComponent<BulletStats_ForPlayer>();
            TakeBulletDamage(BSFP);
        }
    }
}
