using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovementEnemy : MonoBehaviour
{
    PlayerManager the_PM;
    [SerializeField]
    GetRandomPoint the_GRP;
    public EnemyBasicStats the_EBS;
    [SerializeField]
    internal NavMeshAgent agent;

    Vector3 new_Pos;
    Vector3 player_Pos;

    public float unit_Charging_Time = 2;
    public float unit_Current_Charging_Time;

    [SerializeField]
    bool is_Hit;

    enum unit_Task
    {
        Patrolling,
        Attacking,
        Resting
    }
    unit_Task current_UT;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = the_EBS.unit_Speed;
        unit_Current_Charging_Time = unit_Charging_Time;
        the_GRP.GetPoint();
    }
    public void Update()
    {
        CurrentState();
    }

    void CurrentState()
    {
        switch (current_UT)
        {
            case unit_Task.Patrolling:
                {
                    print("Patrolling");
                    if (agent.remainingDistance <= agent.stoppingDistance && the_PM == null)
                    {
                        the_GRP.GetPoint();
                    }
                    else
                    {
                        current_UT = unit_Task.Attacking;
                    }
                    break;
                }
            case unit_Task.Attacking:
                {
                    print("Attacking");
                    if (agent.remainingDistance <= agent.stoppingDistance & the_PM != null)
                    {
                        agent.destination = player_Pos;
                        current_UT = unit_Task.Resting;
                    }
                    else if (the_PM == null)
                    {
                        current_UT = unit_Task.Patrolling;
                    }
                    break;
                }
            case unit_Task.Resting:
                {
                    if (unit_Current_Charging_Time >= 0)
                    {
                        unit_Current_Charging_Time -= Time.deltaTime;
                    }
                    else if (unit_Current_Charging_Time <= 0)
                    {
                        if (the_PM != null)
                        {
                            agent.isStopped = false;
                            player_Pos = the_PM.transform.position;
                            current_UT = unit_Task.Attacking;
                        }
                        else
                        {
                            agent.isStopped = false;
                            unit_Current_Charging_Time = unit_Charging_Time;
                            current_UT = unit_Task.Patrolling;
                        }

                    }
                    break;
                }
        }
        
    }
    /*public void GetPoint()
    {
        Vector3 pos = transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pos,out hit,1,NavMesh.AllAreas))
        {
            new_Pos = hit.position;
            agent.destination = new_Pos;
        }
        else
        {
            GetPoint();
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            the_PM = other.GetComponent<PlayerManager>();
            player_Pos = the_PM.transform.position;
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
