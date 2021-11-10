using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    [SerializeField]
    internal BoxCollider fire_HitBox;
    [SerializeField]
    ParticleSystem fire_VFX;
    [SerializeField]
    float fire_Damage;

    PlayerManager the_PM;
    internal TurrentHead the_TH;

    void HurtPlayer()
    {
        the_PM.TakeDamage(fire_Damage);
    }
    internal void ResetFlameThrower()
    {
        CancelInvoke("HurtPlayer");
        fire_HitBox.enabled = false;
        fire_VFX.Stop();
        the_PM = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            the_PM = other.GetComponent<PlayerManager>();
            InvokeRepeating("HurtPlayer", 0, .5f);
            fire_VFX.Play();
        }
    }

}
