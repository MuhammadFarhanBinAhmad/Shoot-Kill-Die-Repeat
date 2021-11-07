using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovementAI : MonoBehaviour
{
    public EnemyBasicStats the_EBS;

    NavMeshAgent agent;
    public GetPoint GP;
    public float radius;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = the_EBS.unit_Speed;
    }

    private void FixedUpdate()
    {
        if (!agent.hasPath)
        {
            agent.SetDestination(GP.Instance.GetRandomPoint());
            //transform.LookAt(GP.Instance.GetRandomPoint());
            //target located and lock
        }
        if (agent.velocity != Vector3.zero)
        {
            print("moving");
        }
        else
        {
            print("is not moving");
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

#endif 
}
