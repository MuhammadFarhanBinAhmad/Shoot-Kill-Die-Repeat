using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetectPlayer : MonoBehaviour
{
    [SerializeField]
    TurrentHead the_TH;
    [SerializeField]
    SphereCollider _enemy_Attacking_Range;
    [SerializeField]
    bool is_Melee, is_Range;
    // Update is called once per frame
    void Update()
    {
        if (the_TH !=null)
        {
            if (the_TH.current_Target !=null)
            {
                Vector3 direction = (the_TH.current_Target.position - transform.position).normalized;//direction to face desired object
                RaycastHit hit;

                if (Physics.Raycast(transform.position, direction, out hit, 10))
                {
                    if (hit.transform.tag == "Player")
                    {
                        print("HitPlayer");
                        the_TH.target_Lock = true;
                    }
                    else
                    {
                        print(hit.transform.name);
                        the_TH.target_Lock = false;
                    }
                }
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * _enemy_Attacking_Range.radius;
        Gizmos.DrawRay(transform.position, direction);
    }
}
