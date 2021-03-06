using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float accuracy_Reset_Time;
    public float current_Accuracy_Reset_Timing;

    string round_Type_Name;
    bool currently_Shooting;
    bool currently_Reloading;
    [Header("Barrel & Bullet")]
    public GameObject bullet;
    public Transform bullet_Spawn_Point;
    [Header("WeaponSoundEffect")]
    public List<AudioClip> sfx_Weapon_Sound;
    [SerializeField]
    RectTransform the_Crosshair_RectTransform;
    [Header("VFX")]
    public GameObject round_Catridge;
    public Transform transform_Catridge_Spawn_Pos;
    [Header("Animation")]
    public Animator weapon_Anim;
    [Header("UI")]
    public Image ammo_Circle;

    [Header("SpecialAttribute")]
    [SerializeField] internal bool burstfire;
    int rounds_To_Fire = 3;
    int rounds_Already_Fire;

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
        //Set weapon hierarchy
        go_current_Weapon_Equipped = current_WM_Installed[current_Weapon_Equipped].weapon_GameObject;
        GameObject GO = Instantiate(go_current_Weapon_Equipped, transform.position, transform.rotation);
        GO.transform.parent = GameObject.Find("t_Pistol").transform;
        GO.transform.localPosition = new Vector3(0, 0, 0);
        WM_Installed_GameObject.Add(GO);
        go_current_Weapon_Equipped = GO;
        weapon_Anim = GO.GetComponent<Animator>();
        //AmmoCircle
        ammo_Circle = GO.transform.Find("Round/Canvas/Ammo").GetComponent<Image>();
        //Set Weapon bullet pos
        bullet_Spawn_Point = go_current_Weapon_Equipped.transform.Find("SpawnBullet_Pos").transform;
        bullet_Spawn_Point.transform.position = go_current_Weapon_Equipped.transform.Find("SpawnBullet_Pos").position;
        transform_Catridge_Spawn_Pos = go_current_Weapon_Equipped.transform.Find("SpawnCatridge_Pos").transform;
        transform_Catridge_Spawn_Pos.transform.position = go_current_Weapon_Equipped.transform.Find("SpawnCatridge_Pos").position;

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
        if (current_Accuracy_Reset_Timing <= accuracy_Reset_Time)
        {
            current_Accuracy_Reset_Timing += Time.deltaTime;
            the_Crosshair_RectTransform.sizeDelta = new Vector2(Mathf.Lerp(the_Crosshair_RectTransform.rect.width, 20, .1f), Mathf.Lerp(the_Crosshair_RectTransform.rect.width, 20, .1f));
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
            ammo_Circle = GO.transform.Find("Round/Canvas/Ammo").GetComponent<Image>();
            weapon_Anim = GO.GetComponent<Animator>();
            bullet_Spawn_Point = go_current_Weapon_Equipped.transform.Find("SpawnBullet_Pos").transform;
            transform_Catridge_Spawn_Pos = go_current_Weapon_Equipped.transform.Find("SpawnCatridge_Pos").transform;
            burstfire = current_WM_Installed[current_Weapon_Equipped].burstfire;

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
                        if (burstfire)
                        {
                            StartCoroutine("BurstFireShooting");
                            current_WM_Installed[current_Weapon_Equipped].next_Time_To_Fire = Time.time + 1f / current_WM_Installed[current_Weapon_Equipped].fire_Rate;
                        }
                        else
                        {
                            ShootWeapon();
                            current_WM_Installed[current_Weapon_Equipped].next_Time_To_Fire = Time.time + 1f / current_WM_Installed[current_Weapon_Equipped].fire_Rate;
                        }
                        
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
            IngameUIUpdate();
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
    void SpawnMuzzleFlash()
    {
        for (int i = 0; i < the_Ammo_Pool.bullet_Player_Pool.Count; i++)
        {
            if (!the_Ammo_Pool.muzzle_Flash_Spark_Pool[i].activeInHierarchy)
            {
                the_Ammo_Pool.muzzle_Flash_Spark_Pool[i].transform.position = bullet_Spawn_Point.transform.position;
                the_Ammo_Pool.muzzle_Flash_Spark_Pool[i].transform.rotation = bullet_Spawn_Point.transform.rotation;
                the_Ammo_Pool.muzzle_Flash_Spark_Pool[i].GetComponent<AudioSource>().clip = sfx_Weapon_Sound[current_WM_Installed[current_Weapon_Equipped].weapon_Code];
                the_Ammo_Pool.muzzle_Flash_Spark_Pool[i].SetActive(true);
                break;
            }

        }
    }
    void IngameUIUpdate()
    {
        ammo_Circle.fillAmount = (((float)current_WM_Installed[current_Weapon_Equipped].gun_current_Mag_Capacity / (float)current_WM_Installed[current_Weapon_Equipped].gun_Total_Mag_Capacity));
    }
    void ImplementWeaponUpgrades(int i)
    {
        int WC = current_WM_Installed[current_Weapon_Equipped].weapon_Code;
        switch (WC)
        {
            //Pistol Upgrade
            case 0:
                {
                    print("PistolSpecialUpgrade");
                    the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().SpecialPistolUpgrade(current_WM_Installed[current_Weapon_Equipped].pistol_Upgrade_Type);
                    print("BulletStats_ForPlayer PistolUpgradeType = " + current_WM_Installed[current_Weapon_Equipped].pistol_Upgrade_Type);
                    break;
                }
            //SMG Upgrade
            case 1:
                {
                    print("SMGSpecialUpgrade");
                    the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().SpecialSMGUpgrade(current_WM_Installed[current_Weapon_Equipped].SMG_Upgrade_Type);
                    print("BulletStats_ForPlayer SMGUpgradeType = " + current_WM_Installed[current_Weapon_Equipped].SMG_Upgrade_Type);
                    break;
                }
            //Rifle Upgrade
            case 2:
                {
                    print("RifleSpecialUpgrade");
                    if (current_WM_Installed[current_Weapon_Equipped].Rifle_Upgrade_Type != 0)
                    {
                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().SpecialRifleUpgrade(current_WM_Installed[current_Weapon_Equipped].Rifle_Upgrade_Type);
                        print("BulletStats_ForPlayer RifleUpgradeType = " + current_WM_Installed[current_Weapon_Equipped].Rifle_Upgrade_Type);
                    }

                    break;
                }
            //ShotGun Upgrade
            case 3:
                {
                    print("ShotGunUSpecialUpgrade");
                    the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().SpecialShotGunUpgrade(current_WM_Installed[current_Weapon_Equipped].ShotGun_Upgrade_Type);
                    print("BulletStats_ForPlayer ShotGunUpgradeType = " + current_WM_Installed[current_Weapon_Equipped].ShotGun_Upgrade_Type);
                    break;
                }
            //AssaultRifle Upgrade
            case 4:
                {
                    print("AssaultRifleSpecialUpgrade");
                    the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().SpecialAssaultRifleUpgrade(current_WM_Installed[current_Weapon_Equipped].AssaultRifle_Upgrade_Type);
                    print("BulletStats_ForPlayer AssaultRifleUpgradeType = " + current_WM_Installed[current_Weapon_Equipped].AssaultRifle_Upgrade_Type);
                    break;
                }
            //HMG Upgrade
            case 5:
                {
                    print("HMGSpecialUpgrade");
                    the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().SpecialHMGUpgrade(current_WM_Installed[current_Weapon_Equipped].HMG_Upgrade_Type);
                    print("BulletStats_ForPlayer HMGUpgradeType = " + current_WM_Installed[current_Weapon_Equipped].HMG_Upgrade_Type);
                    break;
                }
        }

    }
    IEnumerator BurstFireShooting()
    {
        ShootWeapon();
        yield return new WaitForSeconds(.1f);
        rounds_Already_Fire++;
        if (rounds_Already_Fire <rounds_To_Fire)
        {
            StartCoroutine("BurstFireShooting");
        }
        else
        {
            rounds_Already_Fire = 0;
        }
    }
    void ShootWeapon()
    {
        if (the_Ammo_Pool == null)
        {
            the_Ammo_Pool = FindObjectOfType<AmmoPool>();
        }
        //Stop all reloading
        StopCoroutine("Reloading");
        currently_Reloading = false;

        if (current_WM_Installed[current_Weapon_Equipped].gun_current_Mag_Capacity > 0)
        {
            weapon_Anim.SetTrigger("Shoot");
            if (current_WM_Installed[current_Weapon_Equipped].is_Shotgun == false)
            {

                for (int i = 0; i < the_Ammo_Pool.bullet_Player_Pool.Count; i++)
                {
                    if (!the_Ammo_Pool.bullet_Player_Pool[i].activeInHierarchy)
                    {
                        if (current_Accuracy_Reset_Timing >= accuracy_Reset_Time)
                        {
                            the_Ammo_Pool.bullet_Player_Pool[i].transform.position = bullet_Spawn_Point.transform.position;
                            the_Ammo_Pool.bullet_Player_Pool[i].transform.rotation = bullet_Spawn_Point.transform.rotation;
                        }
                        else
                        {
                            float r_x = Random.Range(-1.5f, 1.5f);
                            float r_y = Random.Range(-1.5f, 1.5f);

                            the_Ammo_Pool.bullet_Player_Pool[i].transform.position = bullet_Spawn_Point.transform.position;

                            //round spread
                            Quaternion q = Quaternion.Euler
                                (bullet_Spawn_Point.transform.eulerAngles.x + r_x,
                                bullet_Spawn_Point.transform.eulerAngles.y + r_y,
                                bullet_Spawn_Point.transform.eulerAngles.z);
                            the_Ammo_Pool.bullet_Player_Pool[i].transform.rotation = q;
                        }

                        current_Accuracy_Reset_Timing = 0;
                        the_Crosshair_RectTransform.sizeDelta = new Vector2(60, 60);

                        SpawnMuzzleFlash();

                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().enabled = true;
                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Active_Time = current_WM_Installed[current_Weapon_Equipped].bullet_Active_Time;
                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Speed= 700;//get damage value
                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Damage = current_WM_Installed[current_Weapon_Equipped].max_Damage;//get damage value
                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().damage_Max = current_WM_Installed[current_Weapon_Equipped].max_Damage;
                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().damage_Min = current_WM_Installed[current_Weapon_Equipped].min_Damage;
                        if (current_WM_Installed[current_Weapon_Equipped].weapon_Special_Upgraded)
                        {
                            ImplementWeaponUpgrades(i);
                            print("ImplementingSpecialUpgrade");
                        }
                        the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().round_Type = current_WM_Installed[current_Weapon_Equipped].current_Round_Type;//set bullet type

                        the_Ammo_Pool.bullet_Player_Pool[i].SetActive(true);

                        for (int c = 0; c < the_Ammo_Pool.bullet_Catridge_Pool.Count; c++)
                        {
                            if (!the_Ammo_Pool.bullet_Catridge_Pool[c].activeInHierarchy)
                            {
                                Vector3 v = new Vector3(transform_Catridge_Spawn_Pos.position.x, transform_Catridge_Spawn_Pos.position.y, transform_Catridge_Spawn_Pos.position.z);
                                the_Ammo_Pool.bullet_Catridge_Pool[c].transform.position = v;
                                the_Ammo_Pool.bullet_Catridge_Pool[c].transform.position = v;
                                the_Ammo_Pool.bullet_Catridge_Pool[c].SetActive(true);
                                the_Ammo_Pool.bullet_Catridge_Pool[c].GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(30,70));
                                break;
                            }
                        }
                        //the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats>().ElementType(the_Element_Type);//set bullet type
                        //the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats>().is_Rocket = is_Rocket;
                        //update Weapon UI
                        current_WM_Installed[current_Weapon_Equipped].gun_current_Mag_Capacity--;
                        //the_Player_UI_HUD.AmmoUpdate(the_Player_Manager.current_Weapon);
                        //muzzle_Flash.GetComponent<ParticleSystem>().Play();
                        the_Ammo_Pool.bullet_Player_Pool[i].gameObject.tag = "HurtEnemy";
                        the_Player_UI_HUD.AmmoUpdateV2();
                        IngameUIUpdate();
                        weapon_Anim.SetTrigger("Shoot");
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

                            the_Ammo_Pool.muzzle_Flash_Spark_Pool[i].transform.position = bullet_Spawn_Point.transform.position;
                            the_Ammo_Pool.muzzle_Flash_Spark_Pool[i].transform.rotation = bullet_Spawn_Point.transform.rotation;
                            the_Ammo_Pool.muzzle_Flash_Spark_Pool[i].GetComponent<AudioSource>().clip = sfx_Weapon_Sound[current_WM_Installed[current_Weapon_Equipped].weapon_Code];

                            the_Ammo_Pool.muzzle_Flash_Spark_Pool[i].SetActive(true);

                            the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().enabled = true;
                            the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Active_Time = current_WM_Installed[current_Weapon_Equipped].bullet_Active_Time;
                            the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Speed = 700;//get damage value
                            the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Damage = current_WM_Installed[current_Weapon_Equipped].max_Damage;//get damage value
                            the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().damage_Max = current_WM_Installed[current_Weapon_Equipped].max_Damage;
                            the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().damage_Min = current_WM_Installed[current_Weapon_Equipped].min_Damage;
                            the_Ammo_Pool.bullet_Player_Pool[i].GetComponent<BulletStats_ForPlayer>().round_Type = current_WM_Installed[current_Weapon_Equipped].current_Round_Type;//set bullet type

                            the_Ammo_Pool.bullet_Player_Pool[i].SetActive(true);
                            for (int c = 0; c < the_Ammo_Pool.bullet_Catridge_Pool.Count; c++)
                            {
                                if (!the_Ammo_Pool.bullet_Catridge_Pool[c].activeInHierarchy)
                                {
                                    Vector3 v = new Vector3(transform_Catridge_Spawn_Pos.position.x, transform_Catridge_Spawn_Pos.position.y, transform_Catridge_Spawn_Pos.position.z);
                                    the_Ammo_Pool.bullet_Catridge_Pool[c].transform.position = v;
                                    the_Ammo_Pool.bullet_Catridge_Pool[c].transform.position = v;
                                    the_Ammo_Pool.bullet_Catridge_Pool[c].SetActive(true);
                                    the_Ammo_Pool.bullet_Catridge_Pool[c].GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(30, 70));
                                    break;
                                }
                            }
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
