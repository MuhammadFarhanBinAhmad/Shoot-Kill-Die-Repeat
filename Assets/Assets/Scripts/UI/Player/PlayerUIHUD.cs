using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIHUD : MonoBehaviour
{
    [SerializeField]
    PlayerManager the_PM;
    //PLAYER HUD//
    public TextMeshProUGUI ui_Current_MAG_Capacity,ui_Weapon_Name, ui_Current_Total_Ammo,ui_Total_Coins;
    public BaseGunV2 the_BGV2;
    //PLAYER GAME UI//
    public GameObject PauseMenu;
    public GameObject GameOverScreen;
    bool menu_Open;
    public TextMeshProUGUI pickable_Weapon_Name_GUI;
    public Image p_HealthBar;

    private void Awake()
    {
        the_BGV2 = the_PM.the_BGV2;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu_Open)
            {
                Cursor.lockState = CursorLockMode.None;
                menu_Open = true;
                PauseMenu.SetActive(true);
                Time.timeScale = 0;

            }
            else if (menu_Open)
            {
                Cursor.lockState = CursorLockMode.Locked;
                menu_Open = false;
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
        ui_Total_Coins.text = "X" + PlayerManager.money_Total.ToString();
    }
    //BASEGUNV2
    internal void AmmoUpdateV2()
    {
        ui_Current_Total_Ammo.text = the_PM.the_BGV2.current_WM_Installed[the_PM.the_BGV2.current_Weapon_Equipped].gun_current_Ammo.ToString();
        ui_Current_MAG_Capacity.text = the_PM.the_BGV2.current_WM_Installed[the_PM.the_BGV2.current_Weapon_Equipped].gun_current_Mag_Capacity.ToString();
    }
    internal void HealthUpdate()
    {
        p_HealthBar.fillAmount = the_PM.health_Player_Current / the_PM.health_Player;
    }
    internal void GameOver()
    {
        GameOverScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }
}
