using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessWeaponExchange : MonoBehaviour
{
    [SerializeField]
    WeaponExchange the_WeaponExchange;
    PlayerManager the_PlayerManager;

    public GameObject WeaponExchange_Store_Page;


    private void Start()
    {
        the_WeaponExchange = GetComponentInParent<WeaponExchange>();
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
        the_PlayerManager.is_Store_Open = true;
        WeaponExchange_Store_Page.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        //Time.timeScale = 0;
    }
    void CloseStorePage()
    {
        the_PlayerManager.is_Store_Open = false;
        WeaponExchange_Store_Page.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        //Time.timeScale = 1;
    }
    void SendWeaponData()
    {
        the_WeaponExchange.players_Gun = the_PlayerManager.the_BGV2;
        the_WeaponExchange.UpdateWeaponExchangeButtonsUI();
    }
    void RemoveWeaponData()
    {
        the_WeaponExchange.players_Gun = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            the_PlayerManager = other.GetComponent<PlayerManager>();
            OpenStorePage();
            SendWeaponData();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            CloseStorePage();
            RemoveWeaponData();
            //the_PlayerManager = null;
        }
    }
}
