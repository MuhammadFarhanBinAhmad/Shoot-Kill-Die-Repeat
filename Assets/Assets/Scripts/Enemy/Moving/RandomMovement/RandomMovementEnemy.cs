using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovementEnemy : MonoBehaviour
{
    [SerializeField]
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
                    if (the_PM == null)
                    {
                        if (agent.remainingDistance <= agent.stoppingDistance)
                        {
                            the_GRP.GetPoint();
                            print("Patrol");
                            return;
                        }
                    }
                    if (the_PM != null)
                    {
                        agent.speed = the_EBS.unit_Speed * 3;
                        current_UT = unit_Task.Attacking;
                        print("Atatck");
                    }
                    break;
                }
            case unit_Task.Attacking:
                {
                    if (the_PM != null)
                    {
                        if (agent.remainingDistance <= agent.stoppingDistance)
                        {

                            agent.destination = player_Pos;
                            current_UT = unit_Task.Resting;
                        }
                    }
                    if (the_PM == null)
                    {
                        agent.speed = the_EBS.unit_Speed;
                        current_UT = unit_Task.Patrolling;
                    }
                    break;
                }
            case unit_Task.Resting:
                {
                    print("Rest");
                    if (unit_Current_Charging_Time >= 0)
                    {
                        unit_Current_Charging_Time -= Time.deltaTime;
                    }
                    if (unit_Current_Charging_Time <= 0)
                    {
                        unit_Current_Charging_Time = unit_Charging_Time; 
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
                            agent.speed = the_EBS.unit_Speed;
                            current_UT = unit_Task.Patrolling;
                        }

                    }
                    break;
                }
        }
        
    }
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
