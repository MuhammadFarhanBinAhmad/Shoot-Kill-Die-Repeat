using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIHUD : MonoBehaviour
{
    [SerializeField]
    PlayerManager the_PM;
    public BaseGunV2 the_BGV2;
    //PLAYER HUD//
    [Header("Ammo")]
    public TextMeshProUGUI ui_Current_MAG_Capacity,
        ui_Weapon_Name, 
        ui_Current_Total_Ammo,
        ui_Total_Coins;
    [Header("SpecialWeaponGauge")]
    public GameObject ui_Special_Gauge_Holder;
    public Image ui_Special_Weapon_Gauge;
    [SerializeField]internal float main_Value;
    [SerializeField] internal float moving_Value;
    //PLAYER GAME UI//
    [Header("Menu")]
    public GameObject PauseMenu;
    public GameObject GameOverScreen;
    bool menu_Open;
    [Header("Health")]
    public Image p_HealthBar;
    public GameObject p_DamageEffect;
    public TextMeshProUGUI t_HealthBar;
    [Header("CrossHair")]
    public GameObject CrossHair;

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
    internal void SetActiveSpecialGauge()
    {
        ui_Special_Gauge_Holder.SetActive(true);
    }
    internal void SpecialWeaponNonRoundsUI()
    {
        ui_Special_Weapon_Gauge.fillAmount = moving_Value / main_Value;
    }
    internal void DamageUI()
    {
        StartCoroutine("DamageEffectUI");
    }
    IEnumerator DamageEffectUI()
    {
        p_DamageEffect.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        p_DamageEffect.SetActive(false);
    }
    internal void HealthUpdate()
    {
        p_HealthBar.fillAmount = the_PM.health_Player_Current / the_PM.health_Player;
        t_HealthBar.text = the_PM.health_Player_Current.ToString();
    }
    internal void GameOver()
    {
        GameOverScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }
}
