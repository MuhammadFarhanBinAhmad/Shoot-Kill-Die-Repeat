using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSensor : MonoBehaviour
{

    [SerializeField]
    RoomInformation the_RoomInformation;

    public GameObject barrier_Entrance;

    private void Start()
    {
        the_RoomInformation = GetComponentInParent<RoomInformation>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            barrier_Entrance.SetActive(true);
            the_RoomInformation.SpawnWave();
            Destroy(gameObject);
        }
    }
}
