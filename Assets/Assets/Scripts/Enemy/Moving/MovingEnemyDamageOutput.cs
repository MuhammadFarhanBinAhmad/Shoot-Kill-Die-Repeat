using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyDamageOutput : MonoBehaviour
{
    public EnemyBasicStats the_EBS;
    public GameObject go_Parent;

    [Header("For Kamikaze")]
    public bool is_Kamikaze;
    public GameObject explosion_VFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            if (is_Kamikaze)
            {
                Instantiate(explosion_VFX, transform.position, transform.rotation);
                the_EBS.TakingDamage((int)the_EBS.unit_Health, this.gameObject);
            }
            else
            {
                other.GetComponent<PlayerManager>().TakeDamage(the_EBS.unit_Damage);
            }
        }
    }
}
