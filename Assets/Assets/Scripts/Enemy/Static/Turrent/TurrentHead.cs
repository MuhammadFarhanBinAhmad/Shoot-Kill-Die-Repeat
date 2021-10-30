using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentHead : MonoBehaviour
{
    /// <summary>
    /// 0 - Scanning - turrent turning 360 degrees trying to find player
    /// 1 - Firing - player is spotted and engaging target
    /// 2 - Finding - lost sight of player and currerntly in high alert mode
    /// </summary>
    /// 
    [Header("*Normal Turrent by default")]
    public EnemyBasicStats the_EBS;
    int current_Mode;

    [Header("Bullet Stats")]
    AmmoPool the_Ammo_Pool;
    public float fire_Rate;
    public Transform bullet_Spawn_Point;
    float next_Time_To_Fire = 0;
    public AudioSource bullet_Sound;
    [Header("FlameThrower")]
    public bool is_Flame_Thrower;
    public DamagePit the_Fire;
    [Header("MachineGun")]
    public GameObject Barrel;
    public bool is_MachineGun;
    [SerializeField] Vector3 barrel_Vector;
    [SerializeField] float barrel_Rot_Speed;
    [SerializeField]float Rev_Up_Time;
    float current_Rev_Up_Time;
    [Header("Time and Target")]
    public float time_Before_Reset;
    public float timer;
    public float rotation_Speed;
    public bool target_Lock;
    public Transform current_Target;

    /*public Light mode_Light;
    float t = 0;
    float min = 25, max = 100;*/

    private void Start()
    {
        the_Ammo_Pool = FindObjectOfType<AmmoPool>();
        timer = time_Before_Reset;
        fire_Rate = the_EBS.unit_FireRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CurrentMode();
    }
    void CurrentMode()
    {
        //Scan area for player
        switch (current_Mode)
        {
            case 0:
                {
                    //RaycastHit hit;

                    transform.Rotate(0, 1, 0);
                    if (timer < time_Before_Reset)
                    {
                        timer = time_Before_Reset;
                    }
                    break;
                }
            case 1:
                {
                    if (current_Target != null)
                    {
                        TargetLock();
                        print("hit2");
                    }
                    else
                    {
                        TargetLost();
                    }

                    /*mode_Light.intensity = Mathf.Lerp(min, max, t);

                    t += .5f;

                    if (t > 4)

                    {
                        float temp = max;
                        max = min;
                        min = temp;
                        t = 0.0f;
                    }*/
                    break;
                }
        }
    }
    void TargetLock()
    {
        //target located and lock
        Vector3 direction = current_Target.position - transform.position;
        Quaternion turrent_Rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, turrent_Rotation, (Time.deltaTime * rotation_Speed));

        timer = time_Before_Reset;//reset timer each time target is lock
        if (!is_MachineGun)
        {
            if (Time.time >= next_Time_To_Fire)
            {
                Shooting();
            }
        }
        else
        {
            RevUp();
        }
        //print("TargetLock");
    }
    //For machinegun only
    void RevUp()
    {
        Barrel.transform.Rotate(barrel_Vector * barrel_Rot_Speed * Time.deltaTime);
        /*if (current_Rev_Up_Time <= Rev_Up_Time)
        {
            current_Rev_Up_Time += Time.deltaTime;
        }*/
        if (Time.time >= next_Time_To_Fire)
        {
            Shooting();
            print("hit");
        }
    }
    void TargetLost()
    {
        //if no target is lock within a period of time, the turrent resets

        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (is_MachineGun)
            {
                barrel_Rot_Speed = 0;
                /*if (current_Rev_Up_Time >= 0)
                {
                    current_Rev_Up_Time -= Time.deltaTime;
                }
                else
                {
                    current_Mode = 0;
                }*/
            }
            else
            {
                current_Mode = 0;
            }
        }
    }
    void Shooting()
    {
        {
            RaycastHit hit;

            if (is_Flame_Thrower)
            {
                the_Fire.the_BC.enabled = true;
                the_Fire.the_Fire_VFX.Play();
            }
            if (Physics.Raycast(transform.position, transform.forward, out hit, 250))
            {
                if (hit.transform == current_Target)
                {
                    if (!is_Flame_Thrower)
                    {
                        next_Time_To_Fire = Time.time + 1f / fire_Rate;
                        for (int i = 0; i < the_Ammo_Pool.bullet_Pool.Count; i++)
                        {
                            if (!the_Ammo_Pool.bullet_Pool[i].activeInHierarchy)
                            {
                                the_Ammo_Pool.bullet_Pool[i].transform.position = bullet_Spawn_Point.transform.position;
                                the_Ammo_Pool.bullet_Pool[i].transform.rotation = bullet_Spawn_Point.transform.rotation;
                                the_Ammo_Pool.bullet_Pool[i].SetActive(true);
                                the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats_ForEnemy>().enabled = true;
                                the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats_ForEnemy>().bullet_Speed = the_EBS.unit_Speed;
                                the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats_ForEnemy>().bullet_Damage = the_EBS.unit_Damage;
                                the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats_ForEnemy>().round_Type = the_EBS.unit_RoundType;
                                the_Ammo_Pool.bullet_Pool[i].gameObject.tag = "HurtPlayer";
                                break;
                            }
                        }
                    }
                }
            }
           
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() !=null)
        {
            current_Target = other.GetComponent<PlayerManager>().transform;
            current_Mode = 1;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null && current_Target != null)
        {
            current_Target=  null;
            if (is_Flame_Thrower)
            {
                the_Fire.the_Fire_VFX.Stop();
                the_Fire.the_BC.enabled = false;
            }
        }
    }
}
