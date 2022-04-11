using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlayer : MonoBehaviour
{

    Rigidbody the_RB;
    [SerializeField] float flame_Speed;

    private void Start()
    {
        the_RB = GetComponent<Rigidbody>();
        Invoke("Destroy", 3);
    }

    private void FixedUpdate()
    {
        the_RB.velocity = transform.forward * Time.deltaTime * flame_Speed;
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyBasicStats>() != null)
        {
            if (other.GetComponent<FireRoundPlayer>() == null)
            {
                other.gameObject.AddComponent<FireRoundPlayer>();
            }
            else
            {
                other.gameObject.GetComponent<FireRoundPlayer>().ResetTimer();
            }
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.tag == ("ForceField"))
        {
            Destroy(gameObject);
        }

    }
}
