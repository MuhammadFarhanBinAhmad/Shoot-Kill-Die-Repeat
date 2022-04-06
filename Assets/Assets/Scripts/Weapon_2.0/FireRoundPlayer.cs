using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRoundPlayer : MonoBehaviour
{

    float timer = 5;
    DamageInput_HurtEnemy the_DIHE;
    private void Start()
    {
        InvokeRepeating("TakingFireDamage", 0, .5f);
        the_DIHE = GetComponent<DamageInput_HurtEnemy>();
    }
    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(GetComponent<FireRoundPlayer>());
        }
    }
    void TakingFireDamage()
    {
        the_DIHE.TakeFireDamage(2);
    }
    internal void ResetTimer()
    {
        timer = 5;
    }
}
