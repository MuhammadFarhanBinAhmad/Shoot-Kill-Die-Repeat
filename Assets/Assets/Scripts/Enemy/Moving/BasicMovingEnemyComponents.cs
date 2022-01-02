using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicMovingEnemyComponents : MonoBehaviour
{

    public PlayerManager the_PM;
    public EnemyBasicStats the_EBS;
    internal NavMeshAgent agent;

    [Header("Patrolling Spot")]
    public List<Transform> check_Point = new List<Transform>();
    public int current_CheckPoint;

    [Header("Attack and Charging Stats")]
    Vector3 player_Pos;
    public float unit_Charging_Time = 2;
    public float unit_Current_Charging_Time;
    public float charge_Speed_Multiplier;
    [SerializeField]
    bool is_Hit;

    enum unit_Task
    {
        Patrolling,
        Attacking,
        ChargingAttack
    }
    unit_Task current_UT;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = the_EBS.unit_Speed;
        unit_Current_Charging_Time = unit_Charging_Time;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        NavMeshHit hit;
        if (NavMesh.SamplePosition(Vector3.zero, out hit, 1000.0f, NavMesh.AllAreas))
        {
            CurrentTask();
        }
    }
    internal void IsKnockBack()
    {
        StartCoroutine("KnockBack");
    }
    IEnumerator KnockBack()
    {
        is_Hit = true;
        agent.speed /= charge_Speed_Multiplier;
        yield return new WaitForSeconds(0.25f);
        is_Hit = false;
    }
    void CurrentTask()
    {
        switch (current_UT)
        {
            case unit_Task.Patrolling:
                {
                    if (agent.speed < the_EBS.unit_Speed && !is_Hit)
                    {
                        agent.speed = the_EBS.unit_Speed;
                    }
                    if (the_PM == null)
                    {
                        {
                            if (agent.remainingDistance <= agent.stoppingDistance)
                            {
                                /*if (current_CheckPoint > check_Point.Count - 1)
                                {
                                    current_CheckPoint = 0;
                                }
                                else*/
                                {
                                    current_CheckPoint++;
                                    if ((current_CheckPoint > check_Point.Count - 1))
                                    {
                                        current_CheckPoint = 0;
                                    }
                                }
                            }
                            agent.destination = check_Point[current_CheckPoint].transform.position;
                            transform.LookAt(check_Point[current_CheckPoint].transform);
                        }
                    }
                    else
                    {
                        current_UT = unit_Task.ChargingAttack;
                        agent.isStopped = true;
                    }
                    break;
                }
            case unit_Task.ChargingAttack:
                {
                    if (the_PM != null)
                    {
                        Vector3 direction = the_PM.transform.position - transform.position;
                        Quaternion turrent_Rotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Lerp(transform.rotation, turrent_Rotation, (Time.deltaTime * 10));
                    }

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
            case unit_Task.Attacking:
                {
                    if (!is_Hit)
                    {
                        agent.speed = the_EBS.unit_Speed * charge_Speed_Multiplier;
                    }
                    else
                    {
                        agent.speed = the_EBS.unit_Speed;
                    }
                    if (the_PM != null)
                    {
                        transform.LookAt(player_Pos);
                        agent.destination = player_Pos;
                        if (agent.remainingDistance <= agent.stoppingDistance)
                        {
                            agent.speed = 0;
                            unit_Current_Charging_Time = unit_Charging_Time;
                            current_UT = unit_Task.ChargingAttack;
                        }

                    }
                    else
                    {
                        the_PM = null;
                        agent.speed = the_EBS.unit_Speed;
                        unit_Current_Charging_Time = unit_Charging_Time;
                        current_UT = unit_Task.Patrolling;
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
