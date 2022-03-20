using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform spawn_pos;
    [SerializeField] Transform  target_pos;
    [SerializeField] GameObject CannonBall;

    internal void FireCannon()
    {
        GameObject C = Instantiate(CannonBall, spawn_pos.transform.position, Quaternion.identity);
        C.GetComponent<CannonProjectile>().target_pos = target_pos;
    }
}
