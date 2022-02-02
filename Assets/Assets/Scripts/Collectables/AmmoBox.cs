using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    int ammo_Type;

    public List<int> ammo_To_Give = new List<int>();//amount of ammo to give

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() !=null)
        {
            PickUpAmmo(other.GetComponent<PlayerManager>());
        }
    }

    void PickUpAmmo(PlayerManager PM)
    {

        //ammo_Type = PM.weapon_Inventory[PM.current_Weapon].the_Weapon_Type_Int;
        //PM.weapon_Inventory[PM.current_Weapon].gun_Total_Ammo += ammo_To_Give[ammo_Type];//give ammo to respective weapon type
        PM.the_BGV2.current_WM_Installed[PM.the_BGV2.current_Weapon_Equipped].gun_current_Ammo += ammo_To_Give[PM.the_BGV2.current_WM_Installed[PM.the_BGV2.current_Weapon_Equipped].weapon_Code];
        if (PM.the_BGV2.current_WM_Installed[PM.the_BGV2.current_Weapon_Equipped].gun_current_Ammo > PM.the_BGV2.current_WM_Installed[PM.the_BGV2.current_Weapon_Equipped].gun_Total_Ammo)
        {
            PM.the_BGV2.current_WM_Installed[PM.the_BGV2.current_Weapon_Equipped].gun_current_Ammo = PM.the_BGV2.current_WM_Installed[PM.the_BGV2.current_Weapon_Equipped].gun_Total_Ammo;
        }
        FindObjectOfType<PlayerUIHUD>().AmmoUpdateV2();
        //PM.weapon_Inventory[PM.current_Weapon].AddAmmo(ammo_To_Give[ammo_Type]);
        Destroy(gameObject);
    }
}
