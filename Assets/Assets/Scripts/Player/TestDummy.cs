using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : MonoBehaviour
{
    public float health;

    internal void TakeDamage(float Dmg)
    {
        health -= Dmg;
    }

}
