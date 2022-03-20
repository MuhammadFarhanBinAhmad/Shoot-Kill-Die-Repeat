using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyDamageOutput : MonoBehaviour
{
    public EnemyBasicStats the_EBS;

    [Header("For Kamikaze")]
    public bool is_Kamikaze;
    public GameObject explosion_VFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            if (is_Kamikaze)
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
                the_EBS.TakingDamage((int)the_EBS.unit_Health, this.gameObject);
            }
            else
            {
                other.GetComponent<PlayerManager>().TakeDamage(the_EBS.unit_Damage);
            }
        }
    }
}
