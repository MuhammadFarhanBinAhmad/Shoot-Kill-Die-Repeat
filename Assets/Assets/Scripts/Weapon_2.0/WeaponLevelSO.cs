using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "BasicStats/WeaponLevel", order = 1)]
public class WeaponLevelSO : ScriptableObject
{
    public string placeholderName;
    //The Element = Current Level/COST
    [Header("FireRate")]
    public int fire_Rate_LEVEL;
    public List<int> fire_Rate_COST;
    public List<float> fire_Rate;
    [Header("ReloadTime")]
    public int reload_Time_LEVEL;
    public List<int> reload_Time_COST;
    public List<float> reload_Time;
    [Header("Damage")]
    public int min_max_Damage_LEVEL;
    public List<int> min_max_Damage_COST;
    public List<int> min_Damage, max_Damage;
    [Header("Ammo/Mag")]
    public int total_Ammo_Mag_LEVEL;
    public List<int> total_Ammo_Mag_COST;
    public List<int> total_Ammo;
    public List<int> mag_Capacity;
    [Header("WeaponType")]
    //0-SemiAuto
    //1-FullAuto
    public int weapon_Firing_Mechanism;
    public bool is_Shotgun;

}
