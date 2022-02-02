using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInput_HurtEnemy : MonoBehaviour
{

    public GameObject explosion_Effect;

    internal EnemyBasicStats the_EBS;
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
            Instantiate(explosion_Effect, transform.position, transform.rotation);
        }
    }
    internal void TakeExplosionDamage(int DMG)
    {
        the_EBS.TakingDamage(DMG, this.gameObject);
        if (the_EBS.unit_Health <= 0)
        {
            Instantiate(explosion_Effect, transform.position, transform.rotation);
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
