using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStats_ForEnemy : MonoBehaviour
{
    public float bullet_Speed;
    public float bullet_Damage;
    public float round_Type;
    public float debuff_Effect_Time;

    Rigidbody the_RB;

    public GameObject acid_Smoke,small_Explosion,large_Explosion;
    //For Rocket Only
    public bool is_Rocket;

    private void Start()
    {
        the_RB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        the_RB.velocity = transform.forward * Time.deltaTime * bullet_Speed;
    }

    void OnEnable()
    {
        Invoke("Destroy", 2.5f);//delete itself after a certain time has pass
    }
    void OnDisable()
    {
        CancelInvoke();
    }
    internal void Destroy()
    {
        bullet_Damage = 0;
        enabled = false;
        gameObject.SetActive(false);
    }
    /// <summary>
    /// *NOTE: IGNORE ALL THIS, THIS ALL IS OUTDATED
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (tag == "HurtPlayer" && other.GetComponent<PlayerManager>())
        {

            PlayerManager PM = other.GetComponent<PlayerManager>();
            PM.TakeDamage(bullet_Damage);

            switch (round_Type)
            {

                case 1:
                    {
                        PM.speed_Debuff_Time = 2;
                        break;
                    }
            }

            Destroy();
        }
        //FOR TEST ONLY
        if (other.tag == "TestDummy")
        {
            Destroy();
        }
    }
}
