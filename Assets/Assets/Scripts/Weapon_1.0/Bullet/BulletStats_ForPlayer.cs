using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStats_ForPlayer : MonoBehaviour
{
    public float bullet_Speed;
    [SerializeField]
    internal int damage_Min, damage_Max;
    [SerializeField]
    internal int bullet_Damage;
    Rigidbody the_RB;

    AmmoPool the_AmmoPool;
    //Round Type
    public int round_Type;
    [SerializeField]
    internal float bullet_Active_Time;
    [SerializeField]
    internal float bullet_Active_Up_Time;
    //Element Type
    public int element_Type;
    public GameObject acid_Smoke, small_Explosion, large_Explosion;
    //For Rocket Only
    public bool is_Rocket;

    private void Start()
    {
        the_RB = GetComponent<Rigidbody>();
        the_AmmoPool = FindObjectOfType<AmmoPool>();
        bullet_Active_Up_Time = bullet_Active_Time;
    }

    private void FixedUpdate()
    {
        the_RB.velocity = transform.forward * Time.deltaTime * bullet_Speed;
        bullet_Active_Up_Time -= Time.deltaTime;
    }

    void OnEnable()
    {
        Invoke("Destroy", bullet_Active_Time);//delete itself after a certain time has pass
    }
    void OnDisable()
    {
        CancelInvoke();
    }
    internal void Destroy()
    {
        bullet_Damage = 0;
        enabled = false;
        bullet_Active_Up_Time = bullet_Active_Time;
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyBasicStats>() != null)
        {
            bullet_Speed = 0;
            for (int i = 0; i < the_AmmoPool.enemy_Hit_Spark_Pool.Count; i++)
            {
                if (!the_AmmoPool.enemy_Hit_Spark_Pool[i].activeInHierarchy)
                {
                    the_AmmoPool.enemy_Hit_Spark_Pool[i].transform.position = this.transform.position;
                    the_AmmoPool.enemy_Hit_Spark_Pool[i].transform.rotation = Quaternion.LookRotation(-transform.forward);
                    the_AmmoPool.enemy_Hit_Spark_Pool[i].SetActive(true);
                    break;
                }
            }
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Wall") || other.gameObject.tag == "ForceField")
        {
            bullet_Speed = 0;
            for (int i = 0; i < the_AmmoPool.misc_Spark_Pool.Count; i++)
            {
                if (!the_AmmoPool.misc_Spark_Pool[i].activeInHierarchy)
                {
                    the_AmmoPool.misc_Spark_Pool[i].transform.position = this.transform.position;
                    the_AmmoPool.misc_Spark_Pool[i].transform.rotation = Quaternion.LookRotation(-transform.forward);
                    the_AmmoPool.misc_Spark_Pool[i].SetActive(true);
                    break;
                }
            }
            if (is_Rocket)
            {
                GameObject REE = Instantiate(large_Explosion, transform.position, transform.rotation);//Rocket Explosion
                                                                                                      //REE.GetComponent<ElementalRocket>().element_Type = element_Type;
                Destroy();
            }
            else if (round_Type == 1)
            {
                GameObject REE = Instantiate(small_Explosion, transform.position, transform.rotation);//Rocket Explosion
                Destroy();
            }
            else
            {

                Destroy();

            }
        }
    }
}
