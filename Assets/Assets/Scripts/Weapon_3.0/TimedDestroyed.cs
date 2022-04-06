using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroyed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("RemoveItem");
    }
    IEnumerator RemoveItem()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
