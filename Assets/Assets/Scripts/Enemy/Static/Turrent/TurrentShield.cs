using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentShield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HurtEnemy")
        {
            if (!other.GetComponent<BulletStats_ForPlayer>().round_Piercing)
            {
                other.GetComponent<BulletStats_ForPlayer>().Destroy();
            }
        }
    }
}
