using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    //Set range of contact for enemy to detect player

    public PlayerManager the_PM;
    public GameObject the_GO;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() !=null)
        {
            the_PM = other.GetComponent<PlayerManager>();
        }
        if ((other.tag == "TestDummy"))
        {
            the_GO = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            the_PM = null;
        }
    }
}
