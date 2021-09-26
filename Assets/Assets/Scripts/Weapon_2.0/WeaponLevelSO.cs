using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "BasicStats/WeaponLevel", order = 1)]
public class WeaponLevelSO : ScriptableObject
{
    public string placeholderName;
    public int weapon_Level;
    public float fire_Rate;
    public float reload_Time;
    public int min_Damage, max_Damage;
    public int total_Ammo;
    public int mag_Capacity;
}
