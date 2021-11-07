using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackMaster : MonoBehaviour
{
    public GameObject Core;
    public GameObject BuffBox;
    [SerializeField]
    float buff_Interval;

    bool is_Buffing;

    public ParticleSystem packmaster_VFX;
    private void Start()
    {
        InvokeRepeating("ActivateBuffBox",3,buff_Interval);
    }

    private void FixedUpdate()
    {
        if (is_Buffing)
        {
            Core.transform.Rotate(0, 15, 0);
        }
        else
        {
            Core.transform.Rotate(0, 5, 0);
        }
    }

    void ActivateBuffBox()
    {
        is_Buffing = true;
        BuffBox.SetActive(true);
        packmaster_VFX.Play();
        StartCoroutine("DeactivateBuffBox");
        //GameObject BB = Instantiate(BuffBox, transform.position, transform.rotation);
    }
    IEnumerator DeactivateBuffBox()
    {
        yield return new WaitForSeconds(1);
        is_Buffing = false;
        BuffBox.SetActive(false);
    }

}
