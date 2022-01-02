using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetRandomPoint : MonoBehaviour
{
    [SerializeField]
    RandomMovementEnemy the_RME;

    Vector3 new_pos;
    public Vector3 size;


    public void GetPoint()
    {
        Vector3 pos = transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pos, out hit, 1, NavMesh.AllAreas))
        {
            new_pos = hit.position;
            the_RME.agent.destination = new_pos;
        }
        else
        {
            GetPoint();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, size);
    }
}
