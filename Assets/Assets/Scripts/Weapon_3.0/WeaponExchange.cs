using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponExchange : MonoBehaviour
{
    public BaseGunV2 players_Gun;

    public void ChangeWeapon(WeaponLevelSO WLSO)
    {
        players_Gun.current_WM_Installed[players_Gun.current_Weapon_Equipped].the_WLSO = WLSO;
        players_Gun.current_WM_Installed[players_Gun.current_Weapon_Equipped].ImplementWeaponData();
        players_Gun.the_Player_UI_HUD.AmmoUpdateV2();
    }
}
