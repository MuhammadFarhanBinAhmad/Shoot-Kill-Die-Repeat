using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGunV2 : MonoBehaviour
{
    //References
    PlayerManager the_Player_Manager;
    public PlayerUIHUD the_Player_UI_HUD;
    AmmoPool the_Ammo_Pool;

    /// -------- WeaponData
    [Header("WeaponData")]
    public int current_Weapon_Equipped;
    public List<WeaponMode> current_WM_Eqipped = new List<WeaponMode>();

    string round_Type_Name;
    bool currently_Shooting;
    bool currently_Reloading;
    [Header("Barrel & Bullet")]
    public GameObject bullet;
    public Transform bullet_Spawn_Point;
    [Header("SFX & VFX")]
    public GameObject muzzle_Flash;

    void Start()
    {
        //ammo pool
        the_Player_Manager = FindObjectOfType<PlayerManager>();
        the_Ammo_Pool = FindObjectOfType<AmmoPool>();
        the_Player_UI_HUD = FindObjectOfType<PlayerUIHUD>();
        the_Player_UI_HUD.AmmoUpdateV2();

    }
    private void Update()
    {
        WeaponFireMode();
        if (current_WM_Eqipped[current_Weapon_Equipped].gun_current_Mag_Capacity < 
            current_WM_Eqipped[current_Weapon_Equipped].gun_Total_Mag_Capacity && 
            Input.GetKeyDown(KeyCode.R) && !currently_Reloading && 
            current_WM_Eqipped[current_Weapon_Equipped].gun_current_Ammo > 0)
        {
            StartReloading();
        }
    }
    void WeaponFireMode()
    {
        //diffent weapon setting
        switch (current_WM_Eqipped[current_Weapon_Equipped].weapon_Firing_Mechanism)
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
                    if (Input.GetMouseButton(0) && Time.time >= current_WM_Eqipped[current_Weapon_Equipped].next_Time_To_Fire)
                    {
                        ShootWeapon();
                        current_WM_Eqipped[current_Weapon_Equipped].next_Time_To_Fire = Time.time + 1f / current_WM_Eqipped[current_Weapon_Equipped].fire_Rate;
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
        if (!current_WM_Eqipped[current_Weapon_Equipped].is_Shotgun)
        {
            yield return new WaitForSeconds(current_WM_Eqipped[current_Weapon_Equipped].reload_Time);
            //count how many ammo spent
            int AU = current_WM_Eqipped[current_Weapon_Equipped].gun_Total_Mag_Capacity - current_WM_Eqipped[current_Weapon_Equipped].gun_current_Mag_Capacity;
            current_WM_Eqipped[current_Weapon_Equipped].gun_current_Ammo -= AU;
            current_WM_Eqipped[current_Weapon_Equipped].gun_current_Mag_Capacity = current_WM_Eqipped[current_Weapon_Equipped].gun_Total_Mag_Capacity;//Refill mag
            the_Player_UI_HUD.AmmoUpdateV2();//Update UI
            currently_Reloading = false;
        }
        else
        {
            if (current_WM_Eqipped[current_Weapon_Equipped].gun_current_Mag_Capacity < current_WM_Eqipped[current_Weapon_Equipped].gun_Total_Mag_Capacity)
            {
                yield return new WaitForSeconds(current_WM_Eqipped[current_Weapon_Equipped].reload_Time);
                //count how many ammo spent
                current_WM_Eqipped[current_Weapon_Equipped].gun_current_Ammo--;
                current_WM_Eqipped[current_Weapon_Equipped].gun_current_Mag_Capacity++;
                the_Player_UI_HUD.AmmoUpdateV2();//Update UI
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

        if (current_WM_Eqipped[current_Weapon_Equipped].gun_current_Mag_Capacity > 0)
        {
            print("hit2");

            if (!current_WM_Eqipped[current_Weapon_Equipped].is_Shotgun)
            {
                print("hit3");
                for (int i = 0; i < the_Ammo_Pool.bullet_Pool.Count; i++)
                {
                    if (!the_Ammo_Pool.bullet_Pool[i].activeInHierarchy)
                    {
                        the_Ammo_Pool.bullet_Pool[i].transform.position = bullet_Spawn_Point.transform.position;
                        the_Ammo_Pool.bullet_Pool[i].transform.rotation = bullet_Spawn_Point.transform.rotation;
                        the_Ammo_Pool.bullet_Pool[i].SetActive(true);
                        the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats_ForPlayer>().enabled = true;
                        the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Speed= 1000;//get damage value
                        the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Damage = Random.Range(current_WM_Eqipped[current_Weapon_Equipped].min_Damage, current_WM_Eqipped[current_Weapon_Equipped].max_Damage);//get damage value
                        the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats_ForPlayer>().round_Type = current_WM_Eqipped[current_Weapon_Equipped].current_Round_Type;//set bullet type
                        //the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats>().ElementType(the_Element_Type);//set bullet type
                        //the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats>().is_Rocket = is_Rocket;
                        //update Weapon UI
                        current_WM_Eqipped[current_Weapon_Equipped].gun_current_Mag_Capacity--;
                        //the_Player_UI_HUD.AmmoUpdate(the_Player_Manager.current_Weapon);
                        the_Player_UI_HUD.AmmoUpdateV2();
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
                            the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Damage = Random.Range(current_WM_Eqipped[current_Weapon_Equipped].min_Damage, current_WM_Eqipped[current_Weapon_Equipped].max_Damage);//get damage value
                            the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats_ForPlayer>().round_Type = current_WM_Eqipped[current_Weapon_Equipped].current_Round_Type; ;//set bullet type
                            //the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats>().ElementType(the_Element_Type);//set bullet type
                            the_Player_UI_HUD.AmmoUpdateV2();
                            the_Ammo_Pool.bullet_Pool[i].gameObject.tag = "HurtEnemy";
                            //update Weapon UI
                            break;
                        }
                    }
                }
                current_WM_Eqipped[current_Weapon_Equipped].gun_current_Mag_Capacity--;
               //the_Player_UI_HUD.AmmoUpdate(the_Player_Manager.current_Weapon);
            }
        }
    }
}
