using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SpecialWeaponUpgradeHolder_NonRound : MonoBehaviour
{

    PlayerUIHUD the_PlayerUIHUD;

    [SerializeField] public bool is_MissleLauncher, is_FlameThrower;

    [Header("ForMissleLauncher")]
    [SerializeField] public float launcher_Rest_Time;
    [SerializeField] public float launcher_Rest_Time_Needed;

    [SerializeField] public GameObject Missle;
    [SerializeField] public Transform missle_Spawn_Pos;


    [Header("ForFlameThrower")]
    [SerializeField] public ParticleSystem effect_Fire;
    [SerializeField] public GameObject collider_Fire;
    [SerializeField] public Transform fire_Spawn_Pos;

    [SerializeField] public float FlameThrower_Rest_Time;
    [SerializeField] public float FlameThrower_Rest_Time_Needed;

    public float fire_Rate;
    public float next_Time_To_Fire = 0;


    public Animator weapon_Anim;

    private void Start()
    {
        weapon_Anim = GetComponentInParent<Animator>();
        the_PlayerUIHUD = FindObjectOfType<PlayerUIHUD>();
        SendSpecialWeaponData();
    }

    private void Update()
    {
        if (is_MissleLauncher)
        {
            if (launcher_Rest_Time >= launcher_Rest_Time_Needed && Input.GetMouseButtonDown(1))
            {
                FireLauncher();
            }
            if (launcher_Rest_Time < launcher_Rest_Time_Needed)
            {
                launcher_Rest_Time += Time.deltaTime;
                SendSpecialWeaponData();
            }
            CallSpecialWeaponData();
        }
        if (is_FlameThrower)
        {
            if (Input.GetMouseButton(1) && FlameThrower_Rest_Time >= 0)
            {
                StartFire();
            }
            else
            {
                if (FlameThrower_Rest_Time<= FlameThrower_Rest_Time_Needed)
                {
                    FlameThrower_Rest_Time += Time.deltaTime;
                }
                effect_Fire.Stop();
            }
            SendSpecialWeaponData();
            CallSpecialWeaponData();
        }
    }
    void SendSpecialWeaponData()
    {
        if (is_MissleLauncher)
        {
            the_PlayerUIHUD.main_Value = launcher_Rest_Time_Needed;
            the_PlayerUIHUD.moving_Value = launcher_Rest_Time;
        }
        if (is_FlameThrower)
        {
            the_PlayerUIHUD.main_Value = FlameThrower_Rest_Time_Needed;
            the_PlayerUIHUD.moving_Value = FlameThrower_Rest_Time;
        }
    }
    void CallSpecialWeaponData()
    {
        if (is_MissleLauncher)
        {
            if (launcher_Rest_Time_Needed >= launcher_Rest_Time)
            {
                the_PlayerUIHUD.SpecialWeaponNonRoundsUI();
            }
        }
        if (is_FlameThrower)
        {
            if (FlameThrower_Rest_Time_Needed >= FlameThrower_Rest_Time)
            {
                the_PlayerUIHUD.SpecialWeaponNonRoundsUI();
            }
        }
    }
    //MISSLELAUNCHER
    void FireLauncher()
    {
        weapon_Anim.SetTrigger("FireMissle");
        launcher_Rest_Time = 0;
        GameObject M = Instantiate(Missle, missle_Spawn_Pos.position, missle_Spawn_Pos.rotation);
        
    }
    //FLAMETHROWER
    void StartFire()
    {
        if (!effect_Fire.isPlaying)
        {
            effect_Fire.Play();
        }
        if (Time.time >= next_Time_To_Fire)
        {
            SpawnCollider();
            next_Time_To_Fire = Time.time + 1f / fire_Rate;
        }
        FlameThrower_Rest_Time -= Time.deltaTime;
    }
    void SpawnCollider()
    {
        float r_x = Random.Range(-5f, 5f);
        float r_y = Random.Range(-5f, 5f);

        Quaternion q = Quaternion.Euler
                                        (fire_Spawn_Pos.transform.eulerAngles.x + r_x,
                                        fire_Spawn_Pos.transform.eulerAngles.y + r_y,
                                        fire_Spawn_Pos.transform.eulerAngles.z);

        GameObject F = Instantiate(collider_Fire, fire_Spawn_Pos.position, q);
    }
}
