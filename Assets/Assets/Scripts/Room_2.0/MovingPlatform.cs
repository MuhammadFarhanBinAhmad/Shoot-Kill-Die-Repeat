using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Moving Spot")]
    public List<Transform> check_Point = new List<Transform>();
    public int current_CheckPoint;


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TestDummy>() != null)
        {
            other.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TestDummy>() != null)
        {
            other.transform.parent = null;
        }
    }
}
