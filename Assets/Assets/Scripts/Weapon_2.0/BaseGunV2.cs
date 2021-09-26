using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGunV2 : MonoBehaviour
{
    //References
    PlayerManager the_Player_Manager;
    //PlayerUIHUD the_Player_UI_HUD;
    AmmoPool the_Ammo_Pool;

    /// -------- WeaponData
    [Header("WeaponData")]
    public WeaponLevelSO[] the_WLSO = new WeaponLevelSO[2];
    public int[] weapon_Type = new int[2];
    public int[] weapon_Mode = new int[2];
    public int[] round_Type = new int[2];
    /// -------- CurrentWeaponInfo
    [Header("CurrentWeaponInfo")]
    public int current_Weapon_Eqipped;
    public int current_Weapon_Type;
    public int current_Weapon_Mode;
    public int current_Weapon_Level;
    public int current_Round_Type;

    string round_Type_Name;
    bool currently_Shooting;
    bool currently_Reloading;
    [Header("Ammo")]
    int gun_Total_Mag_Capacity, gun_Total_Ammo;
    public int gun_current_Mag_Capacity, gun_current_Ammo;
    [Header("Rate & Reload")]
    public float reload_Time;
    public float fire_Rate;
    public float next_Time_To_Fire = 0;
    [Header("Barrel & Bullet")]
    public GameObject bullet;
    public Transform bullet_Spawn_Point;
    [Header("Damage")]
    public int min_Damage, max_Damage;
    [Header("SFX & VFX")]
    public GameObject muzzle_Flash;

    void Start()
    {
        //ammo pool
        the_Player_Manager = FindObjectOfType<PlayerManager>();
        the_Ammo_Pool = FindObjectOfType<AmmoPool>();
        //the_Player_UI_HUD = FindObjectOfType<PlayerUIHUD>();
    }
    private void Update()
    {
        WeaponMode();
        if (gun_current_Mag_Capacity < gun_Total_Mag_Capacity && Input.GetKeyDown(KeyCode.R) && !currently_Reloading && gun_current_Ammo > 0)
        {
            StartReloading();
        }
    }
    void SetUpWeaponUI()
    {
    }
    /// ------- When new Weapon/Round Type and level are added
    internal void AddWeaponLevelStats(WeaponLevelSO WLSO)
    {
        the_WLSO[0] = WLSO;
        ChangeWeaponLevelStats();
    }
    internal void AddWeaponType(int WT,int WM)
    {
        weapon_Type[0] = WT;
        weapon_Mode[0] = WM;
        ChangeWeaponType();
    }
    internal void AddRoundType(int RT)
    {
        round_Type[0] = RT;
        ChangeWeaponLevelStats();
    }

    /// -------- Change weapon Data
    void ChangeWeaponLevelStats()
    {
        current_Weapon_Level = the_WLSO[0].weapon_Level;
        fire_Rate = the_WLSO[0].fire_Rate;
        reload_Time = the_WLSO[0].reload_Time;
        gun_Total_Ammo = the_WLSO[0].total_Ammo;
        gun_Total_Mag_Capacity = the_WLSO[0].mag_Capacity;
        gun_current_Ammo = gun_Total_Ammo;
        gun_current_Mag_Capacity = gun_Total_Mag_Capacity;
        min_Damage = the_WLSO[0].min_Damage;
        max_Damage = the_WLSO[0].max_Damage;
    }
    internal void ChangeWeaponType()
    {
        current_Weapon_Type = weapon_Type[0];
    }
    internal void ChangeRoundType()
    {
        current_Round_Type = round_Type[0];
    }
    void WeaponMode()
    {
        //diffent weapon setting
        switch (current_Weapon_Mode)
        {
            case 0:
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        ShootWeapon();
                    }
                }
                break;
            case 1:
                {
                    if (Input.GetMouseButton(0) && Time.time >= next_Time_To_Fire)
                    {
                        ShootWeapon();
                        next_Time_To_Fire = Time.time + 1f / fire_Rate;
                    }
                }
                break;
        }
    }
    //Reloading Weapon//
    void StartReloading()
    {
        StartCoroutine("Reloading");
        currently_Reloading = true;
    }
    IEnumerator Reloading()
    {
        if (weapon_Type[0] != 4)
        {
            yield return new WaitForSeconds(reload_Time);
            //count how many ammo spent
            int AU = gun_Total_Mag_Capacity - gun_current_Mag_Capacity;
            gun_current_Ammo -= AU;
            gun_current_Mag_Capacity = gun_Total_Mag_Capacity;//Refill mag
            //the_Player_UI_HUD.AmmoUpdateV2(the_Player_Manager.current_Weapon);//Update UI
            currently_Reloading = false;
        }
        else
        {
            if (gun_current_Mag_Capacity < gun_Total_Mag_Capacity)
            {
                yield return new WaitForSeconds(reload_Time);
                //count how many ammo spent
                gun_current_Ammo--;
                gun_current_Mag_Capacity++;
                //the_Player_UI_HUD.AmmoUpdateV2();//Update UI
                StartCoroutine("Reloading");
            }
            else
            {
                currently_Reloading = false;
            }
        }
    }
    void ShootWeapon()
    {
        //Stop all reloading
        StopCoroutine("Reloading");
        currently_Reloading = false;
        print("hit1");

        if (gun_current_Mag_Capacity > 0)
        {
            print("hit2");

            if (weapon_Type[0] != 4)
            {
                print("hit3");
                for (int i = 0; i < the_Ammo_Pool.bullet_Pool.Count; i++)
                {
                    if (!the_Ammo_Pool.bullet_Pool[i].activeInHierarchy)
                    {
                        the_Ammo_Pool.bullet_Pool[i].transform.position = bullet_Spawn_Point.transform.position;
                        the_Ammo_Pool.bullet_Pool[i].transform.rotation = bullet_Spawn_Point.transform.rotation;
                        the_Ammo_Pool.bullet_Pool[i].SetActive(true);
                        the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats>().bullet_Damage = Random.Range(min_Damage, max_Damage);//get damage value
                        the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats>().round_Type = round_Type[0];//set bullet type
                        //the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats>().ElementType(the_Element_Type);//set bullet type
                        //the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats>().is_Rocket = is_Rocket;
                        //update Weapon UI
                        gun_current_Mag_Capacity--;
                        //the_Player_UI_HUD.AmmoUpdateV2(the_Player_Manager.current_Weapon);
                        muzzle_Flash.GetComponent<ParticleSystem>().Play();
                        the_Ammo_Pool.bullet_Pool[i].gameObject.tag = "HurtEnemy";
                        break;
                    }
                }
            }
            // For ShotGun
            else
            {
                for (int SB = 0; SB <= 10; SB++)//spawn multiple rounds
                {
                    for (int i = 0; i < the_Ammo_Pool.bullet_Pool.Count; i++)
                    {
                        if (!the_Ammo_Pool.bullet_Pool[i].activeInHierarchy)
                        {
                            float r_x = Random.Range(-5, 5);
                            float r_y = Random.Range(-5, 5);

                            the_Ammo_Pool.bullet_Pool[i].transform.position = bullet_Spawn_Point.transform.position;

                            //round spread
                            Quaternion q = Quaternion.Euler
                                (bullet_Spawn_Point.transform.eulerAngles.x + r_x,
                                bullet_Spawn_Point.transform.eulerAngles.y + r_y,
                                bullet_Spawn_Point.transform.eulerAngles.z);
                            the_Ammo_Pool.bullet_Pool[i].transform.rotation = q;

                            the_Ammo_Pool.bullet_Pool[i].SetActive(true);
                            the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats>().bullet_Damage = Random.Range(min_Damage, max_Damage);//get damage value
                            the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats>().round_Type = round_Type[0];//set bullet type
                            //the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats>().ElementType(the_Element_Type);//set bullet type
                            the_Ammo_Pool.bullet_Pool[i].gameObject.tag = "HurtEnemy";
                            //update Weapon UI
                            break;
                        }
                    }
                }
                gun_current_Mag_Capacity--;
               //the_Player_UI_HUD.AmmoUpdate(the_Player_Manager.current_Weapon);
            }
        }
    }
}
