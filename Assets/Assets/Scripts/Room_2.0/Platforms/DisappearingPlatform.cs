using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public GameObject the_Platform;
    public float disapper_Time,reappear_Time;
    public bool platform_Disappear;

    IEnumerator PlatformDisappearTimer()
    {
        yield return new WaitForSeconds(disapper_Time);
        the_Platform.SetActive(false);
        platform_Disappear = true;
        StartCoroutine("PlatformReappearTimer");
    }

    IEnumerator PlatformReappearTimer()
    {
        yield return new WaitForSeconds(reappear_Time);
        platform_Disappear = false;
        the_Platform.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() !=null && !platform_Disappear)
        {
            StartCoroutine("PlatformDisappearTimer");
            print("HIT");
        }
    }
}
