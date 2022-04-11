using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissleLauncher : MonoBehaviour
{
    [SerializeField] float launcher_Rest_Time;
    [SerializeField] float launcher_Rest_Time_Needed;

    [SerializeField] GameObject Missle;
    [SerializeField] Transform missle_Spawn_Pos;

    public Animator weapon_Anim;


    // Update is called once per frame
    void Update()
    {
        if (launcher_Rest_Time <=0 && Input.GetMouseButtonDown(1))
        {
            FireLauncher();
        }
        if (launcher_Rest_Time > 0)
        {
            launcher_Rest_Time -= Time.deltaTime;
        }
    }

    void FireLauncher()
    {
        weapon_Anim.SetTrigger("FireMissle");
        launcher_Rest_Time = launcher_Rest_Time_Needed;
        GameObject M = Instantiate(Missle, missle_Spawn_Pos.position, missle_Spawn_Pos.rotation);
    }
}
