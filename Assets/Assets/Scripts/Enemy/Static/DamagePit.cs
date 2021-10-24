using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePit : MonoBehaviour
{
    public float damage_Output;
    //TICK ONLY ONE
    [Header("FirePit")]
    public bool fire_Pit;
    [Header("FirePillar")]
    public bool fire_Pillar;
    public bool fire_Activated;

    PlayerManager the_PM;

    internal BoxCollider the_BC;
    public ParticleSystem the_Fire_VFX;

    private void Start()
    {
        the_BC = GetComponent<BoxCollider>();
        if (fire_Pillar)
        {
            InvokeRepeating("ActivateFire", 0, 2);
        }
    }
    void ActivateFire()
    {
        if (fire_Activated)
        {
            fire_Activated = false;
            the_Fire_VFX.Stop();
        }
        else
        {
            fire_Activated = true;
            the_Fire_VFX.Play();
        }
    }

    void HurtPlayer()
    {
        if (the_PM != null)
        {
            if (fire_Pit)
            {
                the_PM.TakeDamage(damage_Output);
            }
            if (fire_Activated)
            {
                the_PM.TakeDamage(damage_Output);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            the_PM = other.GetComponent<PlayerManager>();
            if (fire_Pillar)
            {
                InvokeRepeating("HurtPlayer", 0, .5f);
            }
            if (fire_Pit)
            {
                InvokeRepeating("HurtPlayer", 0, 1);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            the_PM = null;
        }
    }
}
