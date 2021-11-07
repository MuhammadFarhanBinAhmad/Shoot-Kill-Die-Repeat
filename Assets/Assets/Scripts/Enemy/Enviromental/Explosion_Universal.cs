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
    }
}
