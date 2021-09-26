using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUNINATORV2 : MonoBehaviour
{
    public int weapon_Mode;
    public int weapon_Type;
    public int round_Type;
    public int weapon_Level;

    //WeaponType
    /// <summary>
    /// 0 - Pistol
    /// 1 - SMG
    /// 2 - Rifle
    /// 3 - Assault Rifle
    /// 4 - Shotgun
    /// </summary>
    public List<string> weapon_Type_Name = new List<string>();
    //RoundType
    /// <summary>
    /// 0 - Normal
    /// 1 - Explosive
    /// 2 - Piercing
    /// 3 - Healing
    /// </summary>
    public List<string> round_Type_Name = new List<string>();

    public List<WeaponLevelSO> level_Pistol = new List<WeaponLevelSO>();
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            FindObjectOfType<BaseGunV2>().AddWeaponType(weapon_Type,weapon_Mode);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            FindObjectOfType<BaseGunV2>().AddRoundType(round_Type);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            FindObjectOfType<BaseGunV2>().AddWeaponLevelStats(level_Pistol[weapon_Level]);
        }
    }
}
