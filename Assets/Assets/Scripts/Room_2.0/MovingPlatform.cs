using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Moving Spot")]
    public List<Transform> check_Point = new List<Transform>();
    public int point_Number = 0;
    Transform current_Point;

    public float tolerence, speed, delay;

    float start_delay;

    public bool automatic;

    private void Start()
    {
        if (check_Point.Count > 0)
        {
            current_Point = check_Point[0];
        }
        tolerence = speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (transform.position != current_Point.position)
        {
            MovePlatform();
        }
        else
        {
            UpdateTarget();
        }
    }

    void MovePlatform()
    {
        Vector3 heading = current_Point.position - transform.position;
        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;
        if (heading.magnitude < tolerence)
        {
            transform.position = current_Point.position;
            start_delay = Time.time;
        }
    }

    void UpdateTarget()
    {
        if (automatic)
        {
            if (Time.time - start_delay > delay)
            {
                NextPlatform();
            }
        }
    }
    public void NextPlatform()
    {
        print("NEXTPLATFORM");
        point_Number++;
        if (point_Number >= check_Point.Count)
        {
            point_Number = 0;
        }
        current_Point = check_Point[point_Number];
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerManager>() != null)
        {
            other.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            other.transform.parent = null;
        }
    }
}
