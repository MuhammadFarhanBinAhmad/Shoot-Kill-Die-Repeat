using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStats_ForPlayer : MonoBehaviour
{

    public float bullet_Speed;
    [SerializeField]
    internal int damage_Min, damage_Max;
    [SerializeField]
    internal int bullet_Damage;
    Rigidbody the_RB;

    AmmoPool the_AmmoPool;
    //Round Type
    public int round_Type;
    [SerializeField]
    internal float bullet_Active_Time;
    [SerializeField]
    internal float bullet_Active_Up_Time;

    internal int weapon_Code;
    internal int weapon_Upgrade_Type;

    [Header("SpecialWeaponUpgrade")]
    Vector3 hit_Pos;

    [SerializeField] internal bool round_Piercing;
    [SerializeField] internal bool round_Explosive;
    [SerializeField] GameObject fire_vfx_Prefab;
    GameObject fire_vfx;

    [SerializeField] internal bool round_Fire;

    private void Start()
    {
        the_RB = GetComponent<Rigidbody>();
        the_AmmoPool = FindObjectOfType<AmmoPool>();
        bullet_Active_Up_Time = bullet_Active_Time;
    }

    private void FixedUpdate()
    {
        the_RB.velocity = transform.forward * Time.deltaTime * bullet_Speed;
        bullet_Active_Up_Time -= Time.deltaTime;
    }

    void OnEnable()
    {
        Invoke("Destroy", bullet_Active_Time);//delete itself after a certain time has pass
    }
    void OnDisable()
    {
        CancelInvoke();
    }
    internal void Destroy()
    {
        Vector3 hit_Pos;
        bullet_Damage = 0;
        ResetBulletStats();
        enabled = false;
        bullet_Active_Up_Time = bullet_Active_Time;
        gameObject.SetActive(false);
    }
    void ResetBulletStats()
    {
        round_Explosive = false;
        if (round_Fire)
        {
            round_Fire = false;
            Destroy(fire_vfx);
        }
        round_Fire = false;
        round_Piercing = false;
    }
    internal void SpecialPistolUpgrade(int WP)
    {
        switch(WP)
        {
            case 0:
                {
                    print("PiercingRound");
                    round_Piercing = true;
                    bullet_Damage += 15;
                    break;
                }
            case 1:
                {
                    print("ExplosiveRound");
                    round_Explosive = true;
                    break;
                }
        }
    }
    internal void SpecialSMGUpgrade(int WP)
    {
        switch (WP)
        {
            case 0:
                {
                    print("PiercingRound");
                    round_Piercing = true;
                    bullet_Damage += 15;
                    break;
                }
            case 1:
                {
                    print("FireRound");
                    round_Fire = true;
                    GameObject F_VFX = Instantiate(fire_vfx_Prefab, this.transform.position, this.transform.rotation);
                    fire_vfx = F_VFX;
                    F_VFX.transform.parent = this.transform;
                    break;
                }
        }
    }
    internal void SpecialRifleUpgrade(int WP)
    {
        switch (WP)
        {
            case 0:
                {
                    print("BurstFire");
                    break;
                }
            case 1:
                {
                    print("FireRound");
                    round_Fire = true;
                    break;
                }
        }
    }
    internal void SpecialShotGunUpgrade(int WP)
    {
        switch (WP)
        {
            case 0:
                {
                    print("PiercingRound");
                    round_Piercing = true;
                    bullet_Damage += 15;
                    break;
                }
            case 1:
                {
                    print("FireRound");
                    round_Fire = true;
                    break;
                }
        }
    }
    internal void SpecialAssaultRifleUpgrade(int WP)
    {
        switch (WP)
        {
            case 0:
                {
                    print("Launcher");
                    break;
                }
            case 1:
                {
                    print("FireRound");
                    round_Fire = true;
                    break;
                }
        }
    }
    internal void SpecialHMGUpgrade(int WP)
    {
        switch (WP)
        {
            case 0:
                {
                    print("PiercingRound");
                    round_Piercing = true;
                    bullet_Damage += 15;
                    break;
                }
            case 1:
                {
                    print("FireRound");
                    round_Fire = true;
                    break;
                }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Vector3 p = other.ClosestPoint(transform.position);
        if (other.GetComponent<EnemyBasicStats>() != null)
        {
            bullet_Speed = 0;
            if (round_Explosive)
            {
                for (int i = 0; i < the_AmmoPool.Explosion_Rounds_Pool.Count; i++)
                {
                    if (!the_AmmoPool.Explosion_Rounds_Pool[i].activeInHierarchy)
                    {
                        the_AmmoPool.Explosion_Rounds_Pool[i].transform.position = p;
                        the_AmmoPool.Explosion_Rounds_Pool[i].transform.rotation = Quaternion.LookRotation(-transform.forward);
                        the_AmmoPool.Explosion_Rounds_Pool[i].SetActive(true);
                        break;
                    }
                }
            }
            if (round_Fire)
            {
                if (other.GetComponent<FireRoundPlayer>() ==null)
                {
                    other.gameObject.AddComponent<FireRoundPlayer>();
                    print("AddedFiredamageScript");
                }
                else
                {
                    other.gameObject.GetComponent<FireRoundPlayer>().ResetTimer();
                    print("ResetFiredamageTimer");
                }
            }
            for (int i = 0; i < the_AmmoPool.enemy_Hit_Spark_Pool.Count; i++)
            {
                if (!the_AmmoPool.enemy_Hit_Spark_Pool[i].activeInHierarchy)
                {
                    the_AmmoPool.enemy_Hit_Spark_Pool[i].transform.position = p;
                    the_AmmoPool.enemy_Hit_Spark_Pool[i].transform.rotation = Quaternion.LookRotation(-transform.forward);
                    the_AmmoPool.enemy_Hit_Spark_Pool[i].SetActive(true);
                    break;
                }
            }
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            bullet_Speed = 0;
            if (round_Explosive)
            {
                for (int i = 0; i < the_AmmoPool.Explosion_Rounds_Pool.Count; i++)
                {
                    if (!the_AmmoPool.Explosion_Rounds_Pool[i].activeInHierarchy)
                    {
                        the_AmmoPool.Explosion_Rounds_Pool[i].transform.position = p;
                        the_AmmoPool.Explosion_Rounds_Pool[i].transform.rotation = Quaternion.LookRotation(-transform.forward);
                        the_AmmoPool.Explosion_Rounds_Pool[i].SetActive(true);
                        break;
                    }
                }
            }
            for (int i = 0; i < the_AmmoPool.misc_Spark_Pool.Count; i++)
            {
                if (!the_AmmoPool.misc_Spark_Pool[i].activeInHierarchy)
                {
                    the_AmmoPool.misc_Spark_Pool[i].transform.position = p;
                    the_AmmoPool.misc_Spark_Pool[i].transform.rotation = Quaternion.LookRotation(-transform.forward);
                    the_AmmoPool.misc_Spark_Pool[i].SetActive(true);
                    break;
                }
            }
        }
    }
}
