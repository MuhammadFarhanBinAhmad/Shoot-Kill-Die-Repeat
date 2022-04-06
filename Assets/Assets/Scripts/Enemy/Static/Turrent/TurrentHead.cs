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
    [SerializeField]
    internal int current_Mode;
    [SerializeField]Animator the_Anim;//NOT FOR FLAMETHROWER
    [Header("Bullet Stats")]
    AmmoPool the_Ammo_Pool;
    public float fire_Rate;
    public Transform bullet_Spawn_Point;
    float next_Time_To_Fire = 0;
    [Header("MachineGun")]
    public GameObject Barrel;
    public bool is_MachineGun;
    [SerializeField] Vector3 barrel_Vector;
    [SerializeField] float barrel_Rot_Speed;
    [SerializeField] float Rev_Up_Time;
    float current_Rev_Up_Time;
    [Header("Missle")]
    public bool is_Missle;
    [SerializeField] GameObject _Missle;
    [Header("Time and Target")]
    public float time_Before_Reset;
    public float timer;
    public float rotation_Speed;
    public bool target_Lock;
    public Transform current_Target;
    [SerializeField]
    [Header("Special")]
    public bool is_FireTurret;
    [Header("VFX")]
    public ParticleSystem muzzle_Flash;


    /*Quaternion Q;
    [SerializeField]
    float Rot_1;
    [SerializeField]
    float Rot_2;
    [SerializeField]
    float new_rot_Pos;*/
    /*public Light mode_Light;
    float t = 0;
    float min = 25, max = 100;*/

    private void Start()
    {
        the_Ammo_Pool = FindObjectOfType<AmmoPool>();
        //timer = time_Before_Reset;
        fire_Rate = the_EBS.unit_FireRate;
        the_Anim = GetComponent<Animator>();
        //new_rot_Pos = Rot_1;
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

                    if (current_Target != null)
                    {
                        current_Mode = 1;
                        print("Mode 0");
                    }
                    else
                    {
                        transform.Rotate(0, 1, 0);
                    }
                    break;
                }
            case 1:
                {
                    TargetLock();
                    if (target_Lock)
                    {
                        current_Mode = 2;
                        print("Mode 1");
                    }
                    else
                    {
                        TargetLost();
                        print("TargetLost");
                    }
                    break;
                }
            case 2:
                {
                    TargetLock();
                    if (is_MachineGun)
                    {
                        RevUp();
                    }
                    else
                    {
                        Shooting();
                    }
                    if (!target_Lock)
                    {
                        current_Mode = 1;
                    }
                    break;
                }
        }
    }
    void TargetLock()
    {
        //target located and lock
        ///if (target lock) is left
        /////player can ourrun the turret but the turret will not face the player when hes behind the wall
        ///if (target lock) is left
        ///player cant outrun turret but when behind a wall, the turret will still face the player
        //if (target_Lock)
        if (current_Target != null)
        {
            Vector3 direction = current_Target.position - transform.position;
            Quaternion turrent_Rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, turrent_Rotation, (Time.deltaTime * rotation_Speed));
        }
    }
    //For machinegun only
    void RevUp()
    {
        Barrel.transform.Rotate(barrel_Vector * barrel_Rot_Speed * Time.deltaTime);
        Shooting();

    }
    internal void TargetLost()

    {
        if (is_MachineGun)
        {
            barrel_Rot_Speed = 0;
        }
        current_Mode = 0;
    }
    //if no target is lock within a period of time, the turrent resets

    /*if (timer >= 0)
    {
        timer -= Time.deltaTime;
    }*/

    void Shooting()
    {
        {
            //RaycastHit hit;
            if (Time.time >= next_Time_To_Fire)
            {
                next_Time_To_Fire = Time.time + 1f / fire_Rate;
                for (int i = 0; i < the_Ammo_Pool.bullet_Enemy_Pool.Count; i++)
                {
                    if (is_Missle)
                    {
                        Instantiate(_Missle, bullet_Spawn_Point.position, transform.rotation);
                        the_Anim.SetTrigger("Fire");
                        break;
                    }
                    else
                    {
                        if (!the_Ammo_Pool.bullet_Enemy_Pool[i].activeInHierarchy)
                        {
                            the_Ammo_Pool.bullet_Enemy_Pool[i].transform.rotation = bullet_Spawn_Point.transform.rotation;
                            the_Ammo_Pool.bullet_Enemy_Pool[i].transform.position = bullet_Spawn_Point.transform.position;
                            the_Ammo_Pool.bullet_Enemy_Pool[i].gameObject.tag = "HurtPlayer";
                            the_Ammo_Pool.bullet_Enemy_Pool[i].SetActive(true);
                            the_Ammo_Pool.bullet_Enemy_Pool[i].GetComponent<BulletStats_ForEnemy>().enabled = true;
                            the_Ammo_Pool.bullet_Enemy_Pool[i].GetComponent<BulletStats_ForEnemy>().bullet_Speed = 1000;
                            the_Ammo_Pool.bullet_Enemy_Pool[i].GetComponent<BulletStats_ForEnemy>().bullet_Damage = the_EBS.unit_Damage;
                            the_Ammo_Pool.bullet_Enemy_Pool[i].GetComponent<BulletStats_ForEnemy>().round_Type = the_EBS.unit_RoundType;
                            the_Anim.SetTrigger("Fire");
                            muzzle_Flash.Play();
                            if (is_FireTurret)
                            {
                                the_Ammo_Pool.bullet_Enemy_Pool[i].GetComponent<BulletStats_ForEnemy>().is_FireTurret = true;
                                the_Anim.SetTrigger("Fire");
                            }

                            break;
                        }
                    }
                }
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            current_Target = other.GetComponent<PlayerManager>().transform;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null && current_Target != null)
        {
            current_Target = null;
            target_Lock = false;
        }
    }
}
