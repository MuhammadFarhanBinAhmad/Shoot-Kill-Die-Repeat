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
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletStats_ForPlayer>() != null && other.GetComponent<BulletStats_ForPlayer>().tag == "HurtEnemy")
        {
            //Instantiate(explosion_Effect, transform.position, transform.rotation);
            //FindObjectOfType<Room>().enemy_Left.Remove(this.transform.parent.gameObject);
            //FindObjectOfType<Room>().CheckEnemy();
            the_EBS.TakingDamage(other.GetComponent<BulletStats_ForPlayer>().bullet_Damage);
            if (the_EBS.unit_Health <= 0)
            {
                Instantiate(explosion_Effect, transform.position, transform.rotation);
            }
            other.GetComponent<BulletStats_ForPlayer>().Destroy();
            /*GetComponent<DropCollectables>().SpawnCollectables();
            Destroy(transform.parent.gameObject);*/
        }
        if (other.GetComponent<Explosion_Universal>() !=null)
        {
            print("hit");
            the_EBS.TakingDamage(other.GetComponent<Explosion_Universal>().Damage);
            if (the_EBS.unit_Health <= 0)
            {
                Instantiate(explosion_Effect, transform.position, transform.rotation);
            }
        }
    }
}
