using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePit : MonoBehaviour
{
    //TICK ONLY ONE
    [Header("FirePit")]
    public bool fire_Pit;
    [Header("FirePillar")]
    public bool fire_Pillar;
    public bool fire_Activated;

    public TestDummy the_TD;
    public ParticleSystem the_Fire_VFX;

    private void Start()
    {
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
        if (the_TD !=null)
        {
            if (fire_Pit)
            {
                the_TD.TakeDamage(10);
            }
            if (fire_Activated)
            {
                the_TD.TakeDamage(10);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TestDummy")
        {
            the_TD = other.GetComponent<TestDummy>();
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
        if (other.tag == "TestDummy")
        {
            the_TD = null;
        }
    }
}
