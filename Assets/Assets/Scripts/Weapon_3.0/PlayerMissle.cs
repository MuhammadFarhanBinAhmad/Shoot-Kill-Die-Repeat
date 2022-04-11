using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissle : MonoBehaviour
{

    Rigidbody the_RB;
    public float missle_Speed;
    public GameObject Explosion;

    private void Start()
    {
        the_RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        the_RB.velocity = transform.forward * Time.deltaTime * missle_Speed;
        transform.Rotate(0, 0, 10);
    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 p = other.ClosestPoint(transform.position);

        if (other.GetComponent<EnemyBasicStats>() || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GameObject E = Instantiate(Explosion, p, transform.rotation);
            Destroy(gameObject);
        }
    }
}
