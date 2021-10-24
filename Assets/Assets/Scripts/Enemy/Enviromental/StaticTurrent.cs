using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTurrent : MonoBehaviour
{
    AmmoPool the_Ammo_Pool;


    public Transform bullet_Spawn_Point;
    public int bullet_Damage;

    public float fire_Rate;

    void Start()
    {
        the_Ammo_Pool = FindObjectOfType<AmmoPool>();
        InvokeRepeating("FireProjectile",0,fire_Rate);  
    }

    void FireProjectile()
    {
        for (int i = 0; i < the_Ammo_Pool.bullet_Pool.Count; i++)
        {
            if (!the_Ammo_Pool.bullet_Pool[i].activeInHierarchy)
            {
                the_Ammo_Pool.bullet_Pool[i].transform.position = bullet_Spawn_Point.transform.position;
                the_Ammo_Pool.bullet_Pool[i].transform.rotation = bullet_Spawn_Point.transform.rotation;
                the_Ammo_Pool.bullet_Pool[i].SetActive(true);
                the_Ammo_Pool.bullet_Pool[i].GetComponent<BulletStats_ForPlayer>().bullet_Damage = bullet_Damage;
                the_Ammo_Pool.bullet_Pool[i].gameObject.tag = "HurtPlayer";
                break;
            }
        }
    }
}
