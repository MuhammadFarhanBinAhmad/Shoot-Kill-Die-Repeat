using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    RoomSpawnerV2 RSV2;

    private void Start()
    {
        RSV2 = FindObjectOfType<RoomSpawnerV2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() !=null || other.name == "TestPlayer")
        {
            other.gameObject.transform.position = RSV2.portal_Entrance[RSV2.room_Cleared].transform.Find("SpawnPoint").transform.position;
            RSV2.room_Cleared++;

        }
    }
}
