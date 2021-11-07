using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMine : MonoBehaviour
{
    public GameObject Explosion;

    [SerializeField]
    bool is_Barrel;

    void Explode()
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (is_Barrel)
        {
            if (other.tag == "HurtEnemy")
            {
                Explode();
            }
        }
        else if (other.GetComponent<PlayerManager>() != null || other.tag == "HurtEnemy" || other.GetComponent<Explosion_Universal>() !=null)
        {
          Explode();
        }
    }
}
