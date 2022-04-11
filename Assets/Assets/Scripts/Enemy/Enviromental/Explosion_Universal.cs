using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Universal : MonoBehaviour
{

    public int Damage;
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<PlayerManager>() !=null)
        {
            other.GetComponent<PlayerManager>().TakeDamage(Damage);
        }
        if (other.GetComponent<DamageInput_HurtEnemy>() != null)
        {
            other.GetComponent<DamageInput_HurtEnemy>().TakeExplosionDamage(Damage);

        }
    }
}
