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
        AmmoPool AP = FindObjectOfType<AmmoPool>();
        for (int i = 0; i < AP.Explosion_Universal_Pool.Count; i++)
        {
            if (!AP.Explosion_Universal_Pool[i].activeInHierarchy)
            {
                AP.Explosion_Universal_Pool[i].transform.position = this.transform.position;
                AP.Explosion_Universal_Pool[i].transform.rotation = this.transform.rotation;
                AP.Explosion_Universal_Pool[i].SetActive(true);
                break;
            }
        }
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
