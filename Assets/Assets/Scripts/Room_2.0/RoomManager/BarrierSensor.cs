using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSensor : MonoBehaviour
{

    public GameObject barrier_Entrance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            barrier_Entrance.SetActive(true);
        }
    }
}
