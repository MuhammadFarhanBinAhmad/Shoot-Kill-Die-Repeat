using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public BoxCollider the_Platform;
    public float disapper_Time,reappear_Time;
    public bool platform_Disappear;

    [SerializeField]
    Renderer shader_Dissolving;

    public float smooth = 5;
    public float clip;
    float newvalue;
    [SerializeField]
    float test;
    private void Update()
    {
        if (platform_Disappear && clip<= 1)
        {
            PlatformDissolve();
        }
        if (!platform_Disappear && clip >= 0)
        {
            PlatformDissolve();
        }
    }

    IEnumerator PlatformDisappearTimer()
    {
        yield return new WaitForSeconds(disapper_Time);
        the_Platform.enabled = false;
        newvalue = 1;
        platform_Disappear = true;
        StartCoroutine("PlatformReappearTimer");
    }
    void PlatformDissolve()
    {
        test = Mathf.Lerp(test, newvalue, smooth * Time.deltaTime);
        shader_Dissolving.materials[1].SetFloat("AlphaClip", test);
        shader_Dissolving.materials[2].SetFloat("AlphaClip", test);
    }
    IEnumerator PlatformReappearTimer()
    {
        yield return new WaitForSeconds(reappear_Time);
        the_Platform.enabled = true;
        newvalue = 0;
        platform_Disappear = false;
        //the_Platform.SetActive(true);
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
