using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour
{
    public AnimationCurve curve;
    public Transform target_pos;
    public float speed;
    public GameObject explosion;

    Vector3 end;
    Vector3 start;
    float time;

    void Start()
    {
        start = transform.position;
        end = target_pos.position;
    }


    void Update()
    {
        time += Time.deltaTime * speed;
        Vector3 pos = Vector3.Lerp(start, end, time);
        pos.y += curve.Evaluate(time);
        transform.position = pos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground" || other.GetComponent<PlayerManager>())
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
    }
}
