using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMode : MonoBehaviour
{
    [SerializeField]
    internal WeaponLevelSO the_WLSO;
    public int weapon_Firing_Mechanism;
    internal int round_Type;
    /// -------- CurrentWeaponInfo
    [Header("CurrentWeaponInfo")]
    //public int current_Weapon_Type;
    public int current_Weapon_Mode;
    public int current_Weapon_Level;
    public int current_Round_Type;
    public bool is_Shotgun;
    [Header("Ammo")]
    [SerializeField]
    internal int gun_Total_Mag_Capacity, gun_Total_Ammo;
    public int gun_current_Mag_Capacity, gun_current_Ammo;
    [Header("Rate & Reload")]
    [SerializeField]
    public float reload_Time;
    public float fire_Rate;
    public float next_Time_To_Fire = 0;
    [Header("Damage")]
    public int min_Damage, max_Damage;

    private void Awake()
    {
        current_Weapon_Level = the_WLSO.weapon_Level;
        fire_Rate = the_WLSO.fire_Rate;
        reload_Time = the_WLSO.reload_Time;
        min_Damage = the_WLSO.min_Damage;
        max_Damage = the_WLSO.max_Damage;
        gun_Total_Ammo = the_WLSO.total_Ammo;
        gun_Total_Mag_Capacity = the_WLSO.mag_Capacity;
        gun_current_Ammo = gun_Total_Ammo;
        gun_current_Mag_Capacity = gun_Total_Mag_Capacity;
        weapon_Firing_Mechanism = the_WLSO.weapon_Firing_Mechanism;
    }
}
