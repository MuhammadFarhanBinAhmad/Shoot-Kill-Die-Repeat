using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGunV2 : MonoBehaviour
{
    //References
    PlayerManager the_Player_Manager;
    public PlayerUIHUD the_Player_UI_HUD;
    AmmoPool the_Ammo_Pool;
    AccessGUNINATOR the_AccessGUNINATOR;
    AccessWeaponExchange the_AccessWeaponExchange;

    /// -------- WeaponData
    [Header("WeaponData")]
    public int current_Weapon_Equipped;
    public GameObject go_current_Weapon_Equipped;
    public List<WeaponMode> current_WM_Installed = new List<WeaponMode>();
    public List<GameObject> WM_Installed_GameObject = new List<GameObject>();
    public List<Transform> weapon_Pos = new List<Transform>();

    string round_Type_Name;
    bool currently_Shooting;
    bool currently_Reloading;
    [Header("Barrel & Bullet")]
    public GameObject bullet;
    public Transform bullet_Spawn_Point;
    [Header("SFX & VFX")]
    public GameObject muzzle_Flash;
    [SerializeField]
    Animator the_Anim;

    void Awake()
    {
        //ammo pool
        the_Player_Manager = FindObjectOfType<PlayerManager>();
        the_Ammo_Pool = FindObjectOfType<AmmoPool>();
        the_Player_UI_HUD = FindObjectOfType<PlayerUIHUD>();
        the_AccessGUNINATOR = FindObjectOfType<AccessGUNINATOR>();
        the_AccessWeaponExchange = FindObjectOfType<AccessWeaponExchange>();
        the_Player_UI_HUD.ui_Weapon_Name.text = current_WM_Installed[current_Weapon_Equipped].current_Weapon_Name.ToString();
    }
    private void Start()
    {
        go_current_Weapon_Equipped = current_WM_Installed[current_Weapon_Equipped].weapon_GameObject;
        GameObject GO = Instantiate(go_current_Weapon_Equipped, transform.position, transform.rotation);
        GO.transform.parent = GameObject.Find("t_Pistol").transform;
        GO.transform.localPosition = new Vector3(0, 0, 0);
        WM_Installed_GameObject.Add(GO);
        go_current_Weapon_Equipped = GO;
        bullet_Spawn_Point = go_current_Weapon_Equipped.transform.Find("SpawnBullet_Pos").transform;
        bullet_Spawn_Point.transform.position = go_current_Weapon_Equipped.transform.Find("SpawnBullet_Pos").position;
    }
    private void Update()
    {
        WeaponFireMode();
        if (current_WM_Installed[current_Weapon_Equipped].gun_current_Mag_Capacity < 
            current_WM_Installed[current_Weapon_Equipped].gun_Total_Mag_Capacity && 
            Input.GetKeyDown(KeyCode.R) && !currently_Reloading && 
            current_WM_Installed[current_Weapon_Equipped].gun_current_Ammo > 0)
        {
            StartReloading();
        }
    }
    internal void ChangeWeaponModel()
    {
        if (go_current_Weapon_Equipped != null)
        {
            //Remove current weapon
            Destroy(go_current_Weapon_Equipped);
            go_current_Weapon_Equipped = null;
            //Spawn new weapon
            go_current_Weapon_Equipped = current_WM_Installed[current_Weapon_Equipped].weapon_GameObject;
            GameObject GO = Instantiate(go_current_Weapon_Equipped, transform.position, transform.rotation);
            //Place new place in new position
            GO.transform.parent = weapon_Pos[current_WM_Installed[current_Weapon_Equipped].weapon_Code].transform;
            GO.transform.localPosition = new Vector3(0, 0, 0);


            //Grab all necessary stats/values
            WM_Installed_GameObject[current_Weapon_Equipped] = go_current_Weapon_Equipped;
            go_current_Weapon_Equipped = GO;
            bullet_Spawn_Point = go_current_Weapon_Equipped.transform.Find("SpawnBullet_Pos").transform;
        }

    }
    void WeaponFireMode()
    {
        //diffent weapon setting
        switch (current_WM_Installed[current_Weapon_Equipped].weapon_Firing_Mechanism)
        {
            case 0:
                {
                    if (Input.GetMouseButtonDown(0) && Time.time >= current_WM_Installed[current_Weapon_Equipped].next_Time_To_Fire && !the_Player_Manager.is_Store_Open)
                    {
                        ShootWeapon();
                        current_WM_Installed[current_Weapon_Equipped].next_Time_To_Fire = Time.time + 1f / current_WM_Installed[current_Weapon_Equipped].fire_Rate;
                    }
                }
                break;
            case 1:
                {
                    if (Input.GetMouseButton(0) && Time.time >= current_WM_Installed[current_Weapon_Equipped].next_Time_To_Fire && !the_Player_Manager.is_Store_Open)
                    {
                        ShootWeapon();
                        current_WM_Installed[current_Weapon_Equipped].next_Time_To_Fire = Time.time + 1f / current_WM_Installed[current_Weapon_Equipped].fire_Rate;
                    }
                }
                break;
        }
        the_Player_UI_HUD.ui_Weapon_Name.text = current_WM_Installed[current_Weapon_Equipped].current_Weapon_Name.ToString();
    }
    //Reloading Weapon//
    void StartReloading()
    {
        StartCoroutine("Reloading");
        currently_Reloading = true;
    }
    IEnumerator Reloading()
    {
        if (!current_WM_Installed[current_Weapon_Equipped].is_Shotgun)
        {
            yield return new WaitForSeconds(current_WM_Installed[current_Weapon_Equipped].reload_Time);
            //count how many ammo spent
            int AU = current_WM_Installed[current_Weapon_Equipped].gun_Total_Mag_Capacity - current_WM_Installed[current_Weapon_Equipped].gun_current_Mag_Capacity;
            current_WM_Installed[current_Weapon_Equipped].gun_current_Ammo -= AU;
            current_WM_Installed[current_Weapon_Equipped].gun_current_Mag_Capacity = current_WM_Installed[current_Weapon_Equipped].gun_Total_Mag_Capacity;//Refill mag
            the_Player_UI_HUD.AmmoUpdateV2();//Update UI
            currently_Reloading = false;
        }
        else
        {
            if (current_WM_Installed[current_Weapon_Equipped].gun_current_Mag_Capacity < current_WM_Installed[current_Weapon_Equipped].gun_Total_Mag_Capacity)
            {
                yield return new WaitForSeconds(current_WM_Installed[current_Weapon_Equipped].reload_Time);
                //count how many ammo spent
                current_WM_Installed[current_Weapon_Equipped].gun_current_Ammo--;
                current_WM_Installed[current_Weapon_Equipped].gun_current_Mag_Capacity++;
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

        if (current_WM_Installed[current_Weapon_Equipped].gun_current_Mag_Capacity > 0)
        {
            the_Anim.SetTrigger("Shoot");
            if (current_WM_Installed[current_Weapon_Equipped].is_Shotgun == false)
            {
                for (int i = 0; i < the_Ammo_Pool.bullet_Player_Pool.Count; i++)
                {
                    if (!the_Ammo_Pool.bullet_Player_Pool[i].activeInHierarchy)
                    {
                        the_Ammo_Pool.bullet_Player_Pool[i].transform.position = bullet_Spawn_Point.transform.position;
                        the_Ammo_Pool.bullet_Player_Pool[i].transform.rotation = bullet_Spawn_Point.transform.rotation;
                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().enabled = true;
                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Active_Time = current_WM_Installed[current_Weapon_Equipped].bullet_Active_Time;
                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Speed= 1000;//get damage value
                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Damage = current_WM_Installed[current_Weapon_Equipped].max_Damage;//get damage value
                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().damage_Max = current_WM_Installed[current_Weapon_Equipped].max_Damage;
                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().damage_Min = current_WM_Installed[current_Weapon_Equipped].min_Damage;
                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().round_Type = current_WM_Installed[current_Weapon_Equipped].current_Round_Type;//set bullet type
                        the_Ammo_Pool.bullet_Player_Pool[i].SetActive(true);
                        //the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats>().ElementType(the_Element_Type);//set bullet type
                        //the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats>().is_Rocket = is_Rocket;
                        //update Weapon UI
                        current_WM_Installed[current_Weapon_Equipped].gun_current_Mag_Capacity--;
                        //the_Player_UI_HUD.AmmoUpdate(the_Player_Manager.current_Weapon);
                        muzzle_Flash.GetComponent<ParticleSystem>().Play();
                        the_Ammo_Pool.bullet_Player_Pool[i].gameObject.tag = "HurtEnemy";
                        the_Player_UI_HUD.AmmoUpdateV2();
                        break;
                    }
                }
            }
            // For ShotGun
            else if (current_WM_Installed[current_Weapon_Equipped].is_Shotgun == true)
            {
                for (int SB = 0; SB <= 10; SB++)//spawn multiple rounds
                {
                    for (int i = 0; i < the_Ammo_Pool.bullet_Player_Pool.Count; i++)
                    {
                        if (!the_Ammo_Pool.bullet_Player_Pool[i].activeInHierarchy)
                        {
                            float r_x = Random.Range(-5, 5);
                            float r_y = Random.Range(-5, 5);

                            the_Ammo_Pool.bullet_Player_Pool[i].transform.position = bullet_Spawn_Point.transform.position;

                            //round spread
                            Quaternion q = Quaternion.Euler
                                (bullet_Spawn_Point.transform.eulerAngles.x + r_x,
                                bullet_Spawn_Point.transform.eulerAngles.y + r_y,
                                bullet_Spawn_Point.transform.eulerAngles.z);
                            the_Ammo_Pool.bullet_Player_Pool[i].transform.rotation = q;

                            the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().enabled = true;
                            the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Active_Time = current_WM_Installed[current_Weapon_Equipped].bullet_Active_Time;
                            the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Speed = 1000;//get damage value
                            the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Damage = current_WM_Installed[current_Weapon_Equipped].max_Damage;//get damage value
                            the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().damage_Max = current_WM_Installed[current_Weapon_Equipped].max_Damage;
                            the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().damage_Min = current_WM_Installed[current_Weapon_Equipped].min_Damage;
                            the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().round_Type = current_WM_Installed[current_Weapon_Equipped].current_Round_Type;//set bullet type
                            the_Ammo_Pool.bullet_Player_Pool[i].SetActive(true);
                            //the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats>().ElementType(the_Element_Type);//set bullet type
                            the_Ammo_Pool.bullet_Player_Pool[i].gameObject.tag = "HurtEnemy";
                            //update Weapon UI
                            break;
                        }
                    }
                }
                current_WM_Installed[current_Weapon_Equipped].gun_current_Mag_Capacity--;
                the_Player_UI_HUD.AmmoUpdateV2();
                //the_Player_UI_HUD.AmmoUpdate(the_Player_Manager.current_Weapon);
            }
        }
    }
}
