using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUNINATORV3 : MonoBehaviour
{
    public List<WeaponMode> WeaponTypes;
    public List<WeaponMode> player_Installed_Weapons = new List<WeaponMode>();
    public WeaponMode selected_Weapon;
    public int selected_Weapon_Code;

    public Image bar_FireRate;
    public Image bar_ReloadTime;
    public Image bar_MinMaxDamage;
    public Image bar_TotalAmmoMag;

    public TextMeshProUGUI cost_FireRate;
    public TextMeshProUGUI cost_ReloadTime;
    public TextMeshProUGUI cost_MinMaxDamage;
    public TextMeshProUGUI cost_TotalAmmoMag;

    [Header("PistolSpecialUpgrades")]
    [SerializeField]int PistolSpecialUpgrades_Cost;
    public TextMeshProUGUI PistolSpecialUpgrades_Text;


    [Header("SMGSpecialUpgrades")]
    [SerializeField] int SMGSpecialUpgrades_Cost;
    public TextMeshProUGUI SMGSpecialUpgrades_Text;


    [Header("RifleSpecialUpgrades")]
    [SerializeField] int RifleSpecialUpgrades_Cost;
    public TextMeshProUGUI RifleSpecialUpgrades_Text;


    [Header("AssaultRifleSpecialUpgrades")]
    [SerializeField] int AssaultRifleSpecialUpgrades_Cost;
    public TextMeshProUGUI AssaultRifleSpecialUpgrades_Text;

    public void UpgradeFireRate()
    {
        if (selected_Weapon.fire_Rate_LEVEL <4 && PlayerManager.money_Total >= selected_Weapon.the_WLSO.fire_Rate_COST[selected_Weapon.fire_Rate_LEVEL])
        {
            PlayerManager.money_Total -= selected_Weapon.the_WLSO.fire_Rate_COST[selected_Weapon.fire_Rate_LEVEL];
            selected_Weapon.UpgradeFireRate();
            WeaponStatBar();
        }
    }
    public void UpgradeReloadTime()
    {
        if (selected_Weapon.reload_Time_LEVEL < 4 && PlayerManager.money_Total >= selected_Weapon.the_WLSO.reload_Time_COST[selected_Weapon.reload_Time_LEVEL])
        {
            PlayerManager.money_Total -= selected_Weapon.the_WLSO.reload_Time_COST[selected_Weapon.reload_Time_LEVEL];
            selected_Weapon.UpgradeReloadTime();
            WeaponStatBar();
        }
    }
    public void UpgradeMinMaxDamage()
    {
        if (selected_Weapon.min_max_Damage_LEVEL < 4 && PlayerManager.money_Total >= selected_Weapon.the_WLSO.min_max_Damage_COST[selected_Weapon.min_max_Damage_LEVEL])
        {
            PlayerManager.money_Total -= selected_Weapon.the_WLSO.min_max_Damage_COST[selected_Weapon.min_max_Damage_LEVEL];
            selected_Weapon.UpgradeMinMaxDamage();
            WeaponStatBar();
        }
    }
    public void UpgradeTotalAmmoMag()
    {
        if (selected_Weapon.total_Ammo_Mag_LEVEL<4 && PlayerManager.money_Total >= selected_Weapon.the_WLSO.total_Ammo_Mag_COST[selected_Weapon.total_Ammo_Mag_LEVEL])
        {
            PlayerManager.money_Total -= selected_Weapon.the_WLSO.total_Ammo_Mag_COST[selected_Weapon.total_Ammo_Mag_LEVEL];
            selected_Weapon.UpgradeTotalAmmoMag();
            FindObjectOfType<PlayerManager>().the_BGV2.the_Player_UI_HUD.AmmoUpdateV2();
            WeaponStatBar();
        }
    }
    internal void WeaponStatBar()
    {
        bar_FireRate.fillAmount = 0.25f * selected_Weapon.fire_Rate_LEVEL;
        bar_ReloadTime.fillAmount = 0.25f * selected_Weapon.reload_Time_LEVEL;
        bar_MinMaxDamage.fillAmount = 0.25f * selected_Weapon.min_max_Damage_LEVEL;
        bar_TotalAmmoMag.fillAmount = 0.25f * selected_Weapon.total_Ammo_Mag_LEVEL;

        if (selected_Weapon.fire_Rate_LEVEL == 4)
        {
            cost_FireRate.text = "MAX LEVEL";
        }
        else
        {
            cost_FireRate.text = "X" + selected_Weapon.the_WLSO.fire_Rate_COST[selected_Weapon.fire_Rate_LEVEL].ToString();
        }

        if (selected_Weapon.reload_Time_LEVEL == 4)
        {
            cost_ReloadTime.text = "MAX LEVEL";
        }
        else
        {
            cost_ReloadTime.text = "X" + selected_Weapon.the_WLSO.reload_Time_COST[selected_Weapon.reload_Time_LEVEL].ToString();
        }

        if (selected_Weapon.min_max_Damage_LEVEL == 4)
        {
            cost_MinMaxDamage.text = "MAX LEVEL";
        }
        else
        {
            cost_MinMaxDamage.text = "X" + selected_Weapon.the_WLSO.min_max_Damage_COST[selected_Weapon.min_max_Damage_LEVEL].ToString();
        }

        if (selected_Weapon.total_Ammo_Mag_LEVEL == 4)
        {
            cost_TotalAmmoMag.text = "MAX LEVEL";
        }
        else
        {
            cost_TotalAmmoMag.text = "X" + selected_Weapon.the_WLSO.total_Ammo_Mag_COST[selected_Weapon.total_Ammo_Mag_LEVEL].ToString();
        }
        switch (selected_Weapon.weapon_Code)
        {
            case 0:
                {
                    if (!selected_Weapon.weapon_Special_Upgraded)
                    {
                        PistolSpecialUpgrades_Text.text = "x" + PistolSpecialUpgrades_Cost.ToString();
                    }
                    else
                    {
                        PistolSpecialUpgrades_Text.text = "WEAPON UPGRADED";
                    }
                    break;
                }
            case 1:
                {
                    if (!selected_Weapon.weapon_Special_Upgraded)
                    {
                        SMGSpecialUpgrades_Text.text = "x" + SMGSpecialUpgrades_Cost.ToString();
                    }
                    else
                    {
                        SMGSpecialUpgrades_Text.text = "WEAPON UPGRADED";
                    }
                    break;
                }
            case 2:
                {
                    if (!selected_Weapon.weapon_Special_Upgraded)
                    {
                        RifleSpecialUpgrades_Text.text = "x" + RifleSpecialUpgrades_Cost.ToString();
                    }
                    else
                    {
                        RifleSpecialUpgrades_Text.text = "WEAPON UPGRADED";
                    }
                    break;
                }
            case 4:
                {
                    if (!selected_Weapon.weapon_Special_Upgraded)
                    {
                        AssaultRifleSpecialUpgrades_Text.text = "x" + AssaultRifleSpecialUpgrades_Cost.ToString();
                    }
                    else
                    {
                        AssaultRifleSpecialUpgrades_Text.text = "WEAPON UPGRADED";
                    }
                    break;
                }
        }


    }
    internal void ClearWeaponList()
    {
        player_Installed_Weapons.Clear();
    }

    public void PistolSpecialUpgrade(int pistol_Upgrade_Type)
    {
        //0 - Piercing Round
        //1 - Explosive Round
        if (!selected_Weapon.weapon_Special_Upgraded && PlayerManager.money_Total >= PistolSpecialUpgrades_Cost)
        {
            selected_Weapon.weapon_Special_Upgraded = true;
            selected_Weapon.PistolSpecialUpgrade(pistol_Upgrade_Type);
            PlayerManager.money_Total -=PistolSpecialUpgrades_Cost;
        }
    }
    public void SMGSpecialUpgrade(int SMG_Upgrade_Type)
    {
        //0 - Piercing Round
        //1 - Fire Round
        if (!selected_Weapon.weapon_Special_Upgraded && PlayerManager.money_Total >= SMGSpecialUpgrades_Cost)
        {
            selected_Weapon.weapon_Special_Upgraded = true;
            selected_Weapon.SMGSpecialUpgrade(SMG_Upgrade_Type);
            print("UpgradeType = " + SMG_Upgrade_Type);
            PlayerManager.money_Total -= SMGSpecialUpgrades_Cost;
        }
    }
    public void RifleSpecialUpgrade(int Rifle_Upgrade_Type)
    {
        //0 - BurstFire
        //1 - Fire Round
        if (!selected_Weapon.weapon_Special_Upgraded && PlayerManager.money_Total >= RifleSpecialUpgrades_Cost)
        {
            selected_Weapon.weapon_Special_Upgraded = true;
            selected_Weapon.RifleSpecialUpgrade(Rifle_Upgrade_Type);
            print("UpgradeType = " + Rifle_Upgrade_Type);
            PlayerManager.money_Total -= RifleSpecialUpgrades_Cost;
        }
    }
    public void AssaultRifleSpecialUpgrade(int AssaultRifle_Upgrade_Type)
    {
        //0 - BurstFire
        //1 - Fire Round
        if (!selected_Weapon.weapon_Special_Upgraded && PlayerManager.money_Total >= AssaultRifleSpecialUpgrades_Cost)
        {
            selected_Weapon.weapon_Special_Upgraded = true;
            selected_Weapon.AssaultRifleSpecialUpgrade(AssaultRifle_Upgrade_Type);
            print("UpgradeType = " + AssaultRifle_Upgrade_Type);
            PlayerManager.money_Total -= AssaultRifleSpecialUpgrades_Cost;
        }
    }
}
