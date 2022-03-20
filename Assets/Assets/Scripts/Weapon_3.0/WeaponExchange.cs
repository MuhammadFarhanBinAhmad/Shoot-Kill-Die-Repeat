using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    Transform player_WeaponModes;
    [SerializeField] int cost_Add_Weapon;
    [SerializeField] TextMeshProUGUI ui_Add_Weapon_Cost;

    [Header("WeaponCost")]
    [SerializeField] List<TextMeshProUGUI> ui_Weapon_Cost = new List<TextMeshProUGUI>();
    [SerializeField] List<WeaponLevelSO> ui_WeaponLevelSO = new List<WeaponLevelSO>();

    private void Start()
    {
        UpdateItemCost();
        ui_Add_Weapon_Cost.text = "x" + cost_Add_Weapon.ToString();
    }

    public void ChangeWeapon(WeaponLevelSO WLSO)
    {
        if (LevelManager.weapon_Unlocked[WLSO.weapon_Code] && players_Gun.current_WM_Installed[players_Gun.current_Weapon_Equipped].weapon_Code != WLSO.weapon_Code)
        {
            players_Gun.current_WM_Installed[players_Gun.current_Weapon_Equipped].the_WLSO = WLSO;
            players_Gun.current_WM_Installed[players_Gun.current_Weapon_Equipped].ImplementWeaponData();
            players_Gun.ChangeWeaponModel();
            players_Gun.the_Player_UI_HUD.AmmoUpdateV2();
            UpdateWeaponExchangeButtonsUI();
        }
    }
    public void UpdateWeaponExchangeButtonsUI()
    {
            list_Exchange_Button[weapon_Eqipped_Code].interactable = true;
            weapon_Eqipped_Code = players_Gun.current_WM_Installed[players_Gun.current_Weapon_Equipped].weapon_Code;
            list_Exchange_Button[weapon_Eqipped_Code].interactable = false;
    }
    public void UpdateItemCost()
    {
        for (int i = 1; i < LevelManager.weapon_Unlocked.Length;i++)
        {
            ui_Weapon_Cost[i].text = "X" + ui_WeaponLevelSO[i].weapon_Cost.ToString();
        }
    }
    public void UnlockWeapon(WeaponLevelSO WLSO)
    {
        if (PlayerManager.money_Total >= WLSO.weapon_Cost && !LevelManager.weapon_Unlocked[WLSO.weapon_Code])
        {
            LevelManager.weapon_Unlocked[WLSO.weapon_Code] = true;
            list_Unlock_Button[WLSO.weapon_Code].image.sprite = image_Unlock_Button;
            list_Unlock_Button[WLSO.weapon_Code].interactable = false;
            PlayerManager.money_Total -= WLSO.weapon_Cost;
            ui_Weapon_Cost[WLSO.weapon_Code].text = "SOLD";
        }
    }
    public void AddWeaponSlot()
    {
        //players_Gun.current_WM_Installed.Add(weapon_Default);
        if (PlayerManager.money_Total >= cost_Add_Weapon)
        {
            PlayerManager.money_Total -= cost_Add_Weapon;
            GameObject weapon_New = Instantiate(weapon_Default.gameObject, transform.position, transform.rotation);
            players_Gun.current_WM_Installed.Add(weapon_New.GetComponent<WeaponMode>());
            players_Gun.WM_Installed_GameObject.Add(weapon_New.GetComponent<WeaponMode>().weapon_GameObject);
            weapon_New.transform.parent = players_Gun.transform.parent;
            cost_Add_Weapon *= 2;
            ui_Add_Weapon_Cost.text = cost_Add_Weapon.ToString();
        }
    }
}
