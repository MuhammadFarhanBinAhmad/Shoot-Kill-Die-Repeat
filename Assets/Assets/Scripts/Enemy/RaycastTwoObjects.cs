using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTwoObjects : MonoBehaviour
{
    public Transform FacingObject;

    private void Update()
    {
        Vector3 direction = (FacingObject.position - transform.position).normalized;//direction to face desired object

        RaycastHit hit;

        if (Physics.Raycast(transform.position,direction,out hit,10))
        {
            if (hit.transform.name == "TEST")
            {
                print("HitTest");
            }
            else
            {
                print("NotTest");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 direction = (FacingObject.position - transform.position).normalized * 10;
        Gizmos.DrawRay(transform.position, direction);
    }
}
