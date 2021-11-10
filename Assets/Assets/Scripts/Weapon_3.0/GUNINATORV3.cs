using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUNINATORV3 : MonoBehaviour
{
    public List<WeaponMode> WeaponTypes;

    public List<WeaponMode> player_Installed_Weapons = new List<WeaponMode>();
    public WeaponMode selected_Weapon;

    public void UpgradeFireRate()
    {
        if (selected_Weapon.fire_Rate_LEVEL <4 && PlayerManager.money_Total >= selected_Weapon.the_WLSO.fire_Rate_COST[selected_Weapon.fire_Rate_LEVEL])
        {
            PlayerManager.money_Total -= selected_Weapon.the_WLSO.fire_Rate_COST[selected_Weapon.fire_Rate_LEVEL];
            selected_Weapon.UpgradeFireRate();
        }
    }
    public void UpgradeReloadTime()
    {
        if (selected_Weapon.reload_Time_LEVEL < 4 && PlayerManager.money_Total >= selected_Weapon.the_WLSO.reload_Time_COST[selected_Weapon.reload_Time_LEVEL])
        {
            PlayerManager.money_Total -= selected_Weapon.the_WLSO.reload_Time_COST[selected_Weapon.reload_Time_LEVEL];
            selected_Weapon.UpgradeReloadTime();
        }
    }
    public void UpgradeMinMaxDamage()
    {
        if (selected_Weapon.min_max_Damage_LEVEL < 4 && PlayerManager.money_Total >= selected_Weapon.the_WLSO.min_max_Damage_COST[selected_Weapon.min_max_Damage_LEVEL])
        {
            PlayerManager.money_Total -= selected_Weapon.the_WLSO.min_max_Damage_COST[selected_Weapon.min_max_Damage_LEVEL];
            selected_Weapon.UpgradeMinMaxDamage();
        }
    }
    public void UpgradeTotalAmmoMag()
    {
        if (selected_Weapon.total_Ammo_Mag_LEVEL<4 && PlayerManager.money_Total >= selected_Weapon.the_WLSO.total_Ammo_Mag_COST[selected_Weapon.total_Ammo_Mag_LEVEL])
        {
            PlayerManager.money_Total -= selected_Weapon.the_WLSO.total_Ammo_Mag_COST[selected_Weapon.total_Ammo_Mag_LEVEL];
            selected_Weapon.UpgradeTotalAmmoMag();
            FindObjectOfType<PlayerManager>().the_BGV2.the_Player_UI_HUD.AmmoUpdateV2();
        }
    }
}
