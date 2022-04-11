using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerPlayer : MonoBehaviour
{

    [SerializeField] ParticleSystem effect_Fire;
    [SerializeField] GameObject collider_Fire;
    [SerializeField] Transform fire_Spawn_Pos;


    public float fire_Rate;
    public float next_Time_To_Fire = 0;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            StartFire();
        }
        else
        {
            effect_Fire.Stop();
        }


    }
    void StartFire()
    {
        if (!effect_Fire.isPlaying)
        {
            effect_Fire.Play();
        }

        if (Time.time >= next_Time_To_Fire)
        {
            SpawnCollider();
            next_Time_To_Fire = Time.time + 1f / fire_Rate;
        }
    }
    void SpawnCollider()
    {
        float r_x = Random.Range(-5f, 5f);
        float r_y = Random.Range(-5f, 5f);

        Quaternion q = Quaternion.Euler
                                        (fire_Spawn_Pos.transform.eulerAngles.x + r_x,
                                        fire_Spawn_Pos.transform.eulerAngles.y + r_y,
                                        fire_Spawn_Pos.transform.eulerAngles.z);

        GameObject F = Instantiate(collider_Fire, fire_Spawn_Pos.position, q);
    }
}
