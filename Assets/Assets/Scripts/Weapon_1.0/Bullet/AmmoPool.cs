using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour
{
    public GameObject bullet_Player;
    public GameObject bullet_Enemy;
    public GameObject spark;
    public GameObject text_Damage;
    public int pooled_Amount = 100;
    internal List<GameObject> bullet_Player_Pool = new List<GameObject>();
    internal List<GameObject> bullet_Enemy_Pool = new List<GameObject>();
    internal List<GameObject> spark_Pool = new List<GameObject>();
    internal List<GameObject> text_Damage_Pool = new List<GameObject>();
    GameObject group_Player_Bullet;
    GameObject group_Enemy_Bullet;
    GameObject group_Spark;
    GameObject group_text_Damage;
    private void Start()
    {
        group_Player_Bullet = new GameObject("BulletPlayerPool");
        group_Enemy_Bullet = new GameObject("BulletEnemyPool");
        group_Spark = new GameObject("SparkPool");
        group_text_Damage = new GameObject("TextDamagePool");

        //creating object pool of ammo game object
        for (int i = 0; i <= pooled_Amount; i++)
        {
            GameObject O = Instantiate(bullet_Player);
            GameObject E = Instantiate(bullet_Enemy);
            GameObject S = Instantiate(spark);
            GameObject TD = Instantiate(text_Damage);
            bullet_Player_Pool.Add(O);
            bullet_Enemy_Pool.Add(E);
            spark_Pool.Add(S);
            text_Damage_Pool.Add(TD);
            O.transform.parent = group_Player_Bullet.transform;
            O.SetActive(false);
            E.transform.parent = group_Enemy_Bullet.transform;
            E.SetActive(false);
            S.transform.parent = group_Spark.transform;
            S.SetActive(false);
            TD.transform.parent = group_text_Damage.transform;
            TD.SetActive(false);
        }
        GameObject.DontDestroyOnLoad(group_Player_Bullet);
        GameObject.DontDestroyOnLoad(group_Enemy_Bullet);
        GameObject.DontDestroyOnLoad(group_Spark);
        GameObject.DontDestroyOnLoad(group_text_Damage);

    }
    public void DestroyAmmoPool()
    {
        for (int i = 0; i <= pooled_Amount; i++)
        {
            Destroy(bullet_Player_Pool[i]);
            Destroy(bullet_Enemy_Pool[i]);
            Destroy(spark_Pool[i]);
            Destroy(text_Damage_Pool[i]);
        }
    }
}
