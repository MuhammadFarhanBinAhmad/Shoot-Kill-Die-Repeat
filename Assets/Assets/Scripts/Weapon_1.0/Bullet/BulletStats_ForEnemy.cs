using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStats_ForEnemy : MonoBehaviour
{
    internal float bullet_Speed;
    public float bullet_Damage;
    public float round_Type;
    public float debuff_Effect_Time;

    Rigidbody the_RB;

    public GameObject acid_Smoke,small_Explosion,large_Explosion;
    //For Rocket Only
    [Header("Special")]
    public bool is_FireTurret;
    [SerializeField]
    GameObject vfx_Fire;

    public bool is_Rocket;
    [SerializeField]
    GameObject vfx_Spawned;

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
        Invoke("Destroy", 4f);//delete itself after a certain time has pass
        if (is_FireTurret)
        {
            GameObject F = Instantiate(vfx_Fire, transform.position, transform.rotation);
            F.transform.parent = this.transform;
            vfx_Spawned = F;
        }
    }
    void OnDisable()
    {
        CancelInvoke();
    }
    internal void Destroy()
    {
        bullet_Damage = 0;
        enabled = false;
        Destroy(vfx_Spawned);
        if (is_FireTurret)
        {
            is_FireTurret = false;
        }
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
                        PM.StartCoroutine("SpeedDebuffEffect");
                        break;
                    }
                case 2:
                    {
                        PM.StartCoroutine("FireDamageEffect");
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
