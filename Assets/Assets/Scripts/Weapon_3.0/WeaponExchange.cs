using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponExchange : MonoBehaviour
{
    public BaseGunV2 players_Gun;

    public void ChangeWeapon(WeaponLevelSO WLSO)
    {
        if (LevelManager.weapon_Unlocked[WLSO.weapon_Code] && players_Gun.current_WM_Installed[players_Gun.current_Weapon_Equipped].weapon_Code != WLSO.weapon_Code)
        {
            players_Gun.current_WM_Installed[players_Gun.current_Weapon_Equipped].the_WLSO = WLSO;
            players_Gun.current_WM_Installed[players_Gun.current_Weapon_Equipped].ImplementWeaponData();
            players_Gun.the_Player_UI_HUD.AmmoUpdateV2();
        }
        else
        {
            print("Weapon not unlocked or of the same type");
        }
    }
    public void UnlockWeapon(WeaponLevelSO WLSO)
    {
        if (PlayerManager.scrap_Total >= WLSO.weapon_Cost && !LevelManager.weapon_Unlocked[WLSO.weapon_Code])
        {
            LevelManager.weapon_Unlocked[WLSO.weapon_Code] = true;
        }
        else
        {
            print("Not enough scrap or is Unlocked");
        }
    }
}
