using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePit : MonoBehaviour
{
    public float damage_Output;
    //TICK ONLY ONE
    [Header("FirePit")]
    public bool is_fire_Pit;
    [Header("FirePillar")]
    public bool is_fire_Pillar;
    public bool fire_Activated;
    [SerializeField]
    internal PlayerManager the_PM;

    internal BoxCollider the_BC;
    public ParticleSystem the_Fire_VFX;

    private void Start()
    {
        the_BC = GetComponent<BoxCollider>();
        if (is_fire_Pillar)
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
            if (is_fire_Pit || fire_Activated)
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
            if (is_fire_Pit)
            {
                InvokeRepeating("HurtPlayer", 0, .5f);
            }
            if (is_fire_Pillar)
            {
                InvokeRepeating("HurtPlayer", 0, .5f);
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
