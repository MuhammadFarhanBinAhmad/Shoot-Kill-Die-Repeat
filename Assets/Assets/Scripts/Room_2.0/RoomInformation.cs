using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInformation : MonoBehaviour
{

    public GameObject portal_Exit;
    // Start is called before the first frame update
    void Start()
    {
        portal_Exit = transform.Find("ExitPortal(NextRoom)").gameObject;
    }
}
