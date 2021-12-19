using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponExchange : MonoBehaviour
{
    public BaseGunV2 players_Gun;

    [Header("WeaponUnlock Buttons")]
    [SerializeField]
    Sprite image_Unlock_Button;
    public List<Button> list_Unlock_Button = new List<Button>();

    [Header("WeaponExchange Buttons")]
    public List<Button> list_Exchange_Button = new List<Button>();
    public int weapon_Eqipped_Code;

    [Header("AddWeaponSlot Buttons")]
    [SerializeField]
    WeaponMode weapon_Default;
    [SerializeField]
    Transform player_WeaponModes;

    public void ChangeWeapon(WeaponLevelSO WLSO)
    {
        if (LevelManager.weapon_Unlocked[WLSO.weapon_Code] && players_Gun.current_WM_Installed[players_Gun.current_Weapon_Equipped].weapon_Code != WLSO.weapon_Code)
        {
            players_Gun.current_WM_Installed[players_Gun.current_Weapon_Equipped].the_WLSO = WLSO;
            players_Gun.current_WM_Installed[players_Gun.current_Weapon_Equipped].ImplementWeaponData();
            players_Gun.the_Player_UI_HUD.AmmoUpdateV2();
            UpdateWeaponExchangeButtonsUI();
        }
        else
        {
            print("Weapon not unlocked or of the same type");
        }
    }
    public void UpdateWeaponExchangeButtonsUI()
    {
            list_Exchange_Button[weapon_Eqipped_Code].interactable = true;
            weapon_Eqipped_Code = players_Gun.current_WM_Installed[players_Gun.current_Weapon_Equipped].weapon_Code;
            list_Exchange_Button[weapon_Eqipped_Code].interactable = false;
    }
    public void UnlockWeapon(WeaponLevelSO WLSO)
    {
        if (PlayerManager.scrap_Total >= WLSO.weapon_Cost && !LevelManager.weapon_Unlocked[WLSO.weapon_Code])
        {
            LevelManager.weapon_Unlocked[WLSO.weapon_Code] = true;
            list_Unlock_Button[WLSO.weapon_Code].image.sprite = image_Unlock_Button;
            list_Unlock_Button[WLSO.weapon_Code].interactable = false;
        }
        else
        {
            print("Not enough scrap or is Unlocked");
        }
    }
    public void AddWeaponSlot()
    {
        //players_Gun.current_WM_Installed.Add(weapon_Default);
        GameObject weapon_New = Instantiate(weapon_Default.gameObject, transform.position, transform.rotation);
        players_Gun.current_WM_Installed.Add(weapon_New.GetComponent<WeaponMode>());
        weapon_New.transform.parent = player_WeaponModes.transform;
    }
}
