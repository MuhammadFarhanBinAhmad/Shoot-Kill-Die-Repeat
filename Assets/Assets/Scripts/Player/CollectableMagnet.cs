using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMagnet : MonoBehaviour
{

    bool player_In_Range;
    PlayerManager the_PM;

    private void FixedUpdate()
    {
        MoveToPlayer();
    }
    void MoveToPlayer()
    {
        if (the_PM != null)
        {
            Vector3 heading = the_PM.transform.position - transform.position;
            transform.position += (heading / heading.magnitude) * the_PM.speed_Collectables_Magnet * Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() !=null)
        {
            the_PM = other.GetComponent<PlayerManager>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (the_PM != null)
        {
            the_PM = null;
        }
    }
}
