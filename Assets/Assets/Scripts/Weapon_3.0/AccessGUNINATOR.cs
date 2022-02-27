using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessGUNINATOR : MonoBehaviour
{
    GUNINATORV3 the_Guninator;
    [SerializeField]
    PlayerManager the_PlayerManager;

    public GameObject GUNINATOR_Store_Page;
    public GameObject CrossHair;
    public GameObject GunHolder;

    private void Start()
    {
        the_Guninator = GetComponentInParent<GUNINATORV3>();
    }
    /*private void Update()
    {
        AccessGUNinator();
    }

    void AccessGUNinator()
    {
        if (the_PlayerManager != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!the_PlayerManager.is_Store_Open)
                {
                    OpenStorePage();
                    SendWeaponData();
                }
                else
                {
                    CloseStorePage();
                }
            }
        }
    }*/
    void OpenStorePage()
    {
        SendWeaponData();
        the_PlayerManager.is_Store_Open = true;
        GUNINATOR_Store_Page.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        //Time.timeScale = 0;
    }
    void CloseStorePage()
    {
        the_PlayerManager.is_Store_Open = false;
        GUNINATOR_Store_Page.SetActive(false);
        the_Guninator.ClearWeaponList();
        Cursor.lockState = CursorLockMode.Locked;
        //Time.timeScale = 1;
    }
    void SendWeaponData()
    {
        for (int i = 0; i <the_PlayerManager.the_BGV2.current_WM_Installed.Count ;i++)
        {
            the_Guninator.player_Installed_Weapons.Add(the_PlayerManager.the_BGV2.current_WM_Installed[i]);//send player install weapon into GUNINATOR
        }
        the_Guninator.selected_Weapon = the_PlayerManager.the_BGV2.current_WM_Installed[the_PlayerManager.the_BGV2.current_Weapon_Equipped];
        the_Guninator.WeaponStatBar();
    }
    void RemoveWeaponData()
    {
        the_Guninator.player_Installed_Weapons.Clear();
    }
    void RemoveWeaponObjectAndUI()
    {

        CrossHair.SetActive(false);
        GunHolder.SetActive(false);
    }
    void AddWeaponObjectAndUI()
    {
        CrossHair.SetActive(true);
        GunHolder.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            the_PlayerManager = other.GetComponent<PlayerManager>();
            GunHolder = the_PlayerManager.gameObject.transform.Find("/Player 1/Main Camera/WeaponCam/Weapons/BaseGun/GunHolder").gameObject;
            RemoveWeaponObjectAndUI();
            OpenStorePage();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            AddWeaponObjectAndUI();
            CloseStorePage();
            RemoveWeaponData();
            //the_PlayerManager = null;
        }
    }
}
