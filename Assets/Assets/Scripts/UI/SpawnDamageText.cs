using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDamageText : MonoBehaviour
{

    Rigidbody the_RB;
    [SerializeField]
    bool dmg = true;
    void Start()
    {
        the_RB = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        the_RB.velocity = transform.up * Time.deltaTime * 25;
        if (dmg)
        {
            StartCoroutine("DestroyGameObject");
        }
        else
            dmg = true;
    }
    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
