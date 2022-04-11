using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMode : MonoBehaviour
{
    [SerializeField]
    internal WeaponLevelSO the_WLSO;
    internal int round_Type;
    /// -------- CurrentWeaponInfo
    [Header("CurrentWeaponInfo")]
    //public int current_Weapon_Type;
    public string current_Weapon_Name;
    public int current_Weapon_Mode;
    public int current_Weapon_Level;
    public int current_Round_Type;
    [Header("Ammo")]
    [SerializeField]
    public int total_Ammo_Mag_LEVEL;
    internal int gun_Total_Mag_Capacity, gun_Total_Ammo;
    public int gun_current_Mag_Capacity, gun_current_Ammo;
    public float bullet_Active_Time;
    [Header("Rate & Reload")]
    [SerializeField]
    public int reload_Time_LEVEL;
    public float reload_Time;
    public int fire_Rate_LEVEL;
    public float fire_Rate;
    public float next_Time_To_Fire = 0;
    [Header("Damage")]
    public int min_max_Damage_LEVEL;
    public int min_Damage, max_Damage;
    [Header("WeaponType")]
    //0-SemiAuto
    //1-FullAuto
    public int weapon_Firing_Mechanism;
    public int weapon_Code;
    public bool is_Shotgun;
    public GameObject weapon_GameObject;


    [SerializeField] internal int pistol_Upgrade_Type;
    [SerializeField] internal int SMG_Upgrade_Type;
    [SerializeField] internal int Rifle_Upgrade_Type;
    [SerializeField] internal int ShotGun_Upgrade_Type;
    [SerializeField] internal int AssaultRifle_Upgrade_Type;
    [SerializeField] internal int HMG_Upgrade_Type;

    [Header("SpecialAttribute")]
    [SerializeField] internal bool weapon_Special_Upgraded;
    [SerializeField] internal bool burstfire;

    private void Awake()
    {
        ImplementWeaponData();
    }

    internal void ImplementWeaponData()
    {
        //Weapon component level
        fire_Rate_LEVEL = the_WLSO.fire_Rate_LEVEL;
        reload_Time_LEVEL = the_WLSO.reload_Time_LEVEL;
        min_max_Damage_LEVEL = the_WLSO.min_max_Damage_LEVEL;
        total_Ammo_Mag_LEVEL = the_WLSO.total_Ammo_Mag_LEVEL;

        //Transfer data
        fire_Rate = the_WLSO.fire_Rate[fire_Rate_LEVEL];
        reload_Time = the_WLSO.reload_Time[reload_Time_LEVEL];
        min_Damage = the_WLSO.min_Damage[min_max_Damage_LEVEL];
        max_Damage = the_WLSO.max_Damage[min_max_Damage_LEVEL];
        gun_Total_Ammo = the_WLSO.total_Ammo[total_Ammo_Mag_LEVEL];
        gun_Total_Mag_Capacity = the_WLSO.mag_Capacity[total_Ammo_Mag_LEVEL];
        bullet_Active_Time = the_WLSO.bullet_Active_Time;
        gun_current_Ammo = gun_Total_Ammo;
        gun_current_Mag_Capacity = gun_Total_Mag_Capacity;

        //Weapon Code
        current_Weapon_Name = the_WLSO.placeholderName;
        weapon_Firing_Mechanism = the_WLSO.weapon_Firing_Mechanism;
        is_Shotgun = the_WLSO.is_Shotgun;
        weapon_Code = the_WLSO.weapon_Code;

        //weapon model
        weapon_GameObject = the_WLSO.weapon_Gameobject;

        //SpecialWeaponAttribute
        weapon_Special_Upgraded = the_WLSO.weapon_Special_Upgraded;
        burstfire = the_WLSO.burstfire;
    }
    public void UpgradeFireRate()
    {
        fire_Rate_LEVEL++;
        fire_Rate = the_WLSO.fire_Rate[fire_Rate_LEVEL];
        current_Weapon_Level++;
    }
    public void UpgradeReloadTime()
    {
        reload_Time_LEVEL++;
        reload_Time = the_WLSO.reload_Time[reload_Time_LEVEL];
        current_Weapon_Level++;
    }
    public void UpgradeMinMaxDamage()
    {
        min_max_Damage_LEVEL++;
        min_Damage = the_WLSO.min_Damage[min_max_Damage_LEVEL];
        max_Damage = the_WLSO.max_Damage[min_max_Damage_LEVEL];
        current_Weapon_Level++;
    }
    public void UpgradeTotalAmmoMag()
    {
        total_Ammo_Mag_LEVEL++;
        gun_Total_Ammo = the_WLSO.total_Ammo[total_Ammo_Mag_LEVEL];
        gun_Total_Mag_Capacity = the_WLSO.mag_Capacity[total_Ammo_Mag_LEVEL];

        gun_current_Ammo = gun_Total_Ammo;
        gun_current_Mag_Capacity = gun_Total_Mag_Capacity;
        current_Weapon_Level++;
    }
    internal void PistolSpecialUpgrade(int PUT)
    {
        pistol_Upgrade_Type = PUT;
    }
    internal void SMGSpecialUpgrade(int SUT)
    {
        SMG_Upgrade_Type = SUT;
    }
    internal void RifleSpecialUpgrade(int RUT)
    {
        if (RUT != 0)
        {
            Rifle_Upgrade_Type = RUT;
        }
        else
        {
            FindObjectOfType<BaseGunV2>().burstfire = true;
            burstfire = true;
        }
    }
    internal void ShotGunSpecialUpgrade(int SGUT)
    {
        ShotGun_Upgrade_Type = SGUT;
    }
    internal void AssaultRifleSpecialUpgrade(int ARUT)
    {
        print("hit");
        switch (ARUT)
        {
            case 0:
                {
                    print("SpawnLauncher");
                    FindObjectOfType<BaseGunV2>().go_current_Weapon_Equipped.GetComponent<SpawnLauncher>().SpawnMissleLauncher();
                    FindObjectOfType<PlayerUIHUD>().SetActiveSpecialGauge();
                    break;
                }
            case 1:
                {
                    print("SpawnFlameThrower");
                    FindObjectOfType<BaseGunV2>().go_current_Weapon_Equipped.GetComponent<SpawnLauncher>().SpawnFlameThrower();
                    FindObjectOfType<PlayerUIHUD>().SetActiveSpecialGauge();
                    break;
                }

        }

        /*if (ARUT !=0)
        {
            AssaultRifle_Upgrade_Type = ARUT;
        }
        else
        {
            print("SpawnLauncher");
            FindObjectOfType<BaseGunV2>().go_current_Weapon_Equipped.GetComponent<SpawnLauncher>().Spawn();
        }*/
    }
    internal void HMGSpecialUpgrade(int HMGUT)
    {
        HMG_Upgrade_Type = HMGUT;
    }
}
