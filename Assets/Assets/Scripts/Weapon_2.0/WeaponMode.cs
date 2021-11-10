using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMode : MonoBehaviour
{
    [SerializeField]
    internal WeaponLevelSO the_WLSO;
    internal int round_Type;
    /// -------- CurrentWeaponInfo
    [Header("CurrentWeaponInfo")]
    //public int current_Weapon_Type;
    public int current_Weapon_Mode;
    public int current_Weapon_Level;
    public int current_Round_Type;
    [Header("Ammo")]
    [SerializeField]
    public int total_Ammo_Mag_LEVEL;
    internal int gun_Total_Mag_Capacity, gun_Total_Ammo;
    public int gun_current_Mag_Capacity, gun_current_Ammo;
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
    public bool is_Shotgun;

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

        gun_current_Ammo = gun_Total_Ammo;
        gun_current_Mag_Capacity = gun_Total_Mag_Capacity;
        weapon_Firing_Mechanism = the_WLSO.weapon_Firing_Mechanism;
        is_Shotgun = the_WLSO.is_Shotgun;
    }
    public void UpgradeFireRate()
    {
        fire_Rate_LEVEL++;
        fire_Rate = the_WLSO.fire_Rate[fire_Rate_LEVEL];
    }
    public void UpgradeReloadTime()
    {
        reload_Time_LEVEL++;
        reload_Time = the_WLSO.reload_Time[reload_Time_LEVEL];

    }
    public void UpgradeMinMaxDamage()
    {
        min_max_Damage_LEVEL++;
        min_Damage = the_WLSO.min_Damage[min_max_Damage_LEVEL];
        max_Damage = the_WLSO.max_Damage[min_max_Damage_LEVEL];
    }
    public void UpgradeTotalAmmoMag()
    {
        total_Ammo_Mag_LEVEL++;
        gun_Total_Ammo = the_WLSO.total_Ammo[total_Ammo_Mag_LEVEL];
        gun_Total_Mag_Capacity = the_WLSO.mag_Capacity[total_Ammo_Mag_LEVEL];

        gun_current_Ammo = gun_Total_Ammo;
        gun_current_Mag_Capacity = gun_Total_Mag_Capacity;

    }
}
