using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour
{
    public GameObject bullet_Player;
    public GameObject bullet_Enemy;
    public GameObject bullet_Catridge;
    public GameObject misc_Spark;
    public GameObject enemy_Hit_Spark;
    public GameObject muzzle_Flash_Spark;
    public GameObject Explosion_Universal;
    public GameObject Explosion_Normal;
    public GameObject Explosion_Rounds;
    public GameObject text_Damage;

    public int pooled_Amount = 100;

    internal List<GameObject> bullet_Player_Pool = new List<GameObject>();
    internal List<GameObject> bullet_Enemy_Pool = new List<GameObject>();
    internal List<GameObject> bullet_Catridge_Pool = new List<GameObject>();
    internal List<GameObject> misc_Spark_Pool = new List<GameObject>();
    internal List<GameObject> enemy_Hit_Spark_Pool = new List<GameObject>();
    internal List<GameObject> muzzle_Flash_Spark_Pool = new List<GameObject>();
    internal List<GameObject> Explosion_Universal_Pool = new List<GameObject>();
    internal List<GameObject> Explosion_Normal_Pool = new List<GameObject>();
    internal List<GameObject> Explosion_Rounds_Pool = new List<GameObject>();
    internal List<GameObject> text_Damage_Pool = new List<GameObject>();

    GameObject group_Player_Bullet;
    GameObject group_Enemy_Bullet;
    GameObject group_Catridge_Bullet;
    GameObject group_misc_Spark;
    GameObject group_enemy_Hit_Spark;
    GameObject group_muzzle_Flash_Spark;
    GameObject group_Explosion_Universal;
    GameObject group_Explosion_Normal;
    GameObject group_Explosion_Rounds;
    GameObject group_text_Damage;

    private void Start()
    {
        group_Player_Bullet = new GameObject("BulletPlayerPool");
        group_Enemy_Bullet = new GameObject("BulletEnemyPool");
        group_Catridge_Bullet = new GameObject("BulletCatridgePool");
        group_misc_Spark = new GameObject("MiscSparkPool");
        group_enemy_Hit_Spark = new GameObject("EnemyHitSparkPool");
        group_muzzle_Flash_Spark = new GameObject("MuzzleFlashPool");
        group_Explosion_Universal = new GameObject("ExplosionUniversalPool");
        group_Explosion_Normal = new GameObject("ExplosionNormalPool");
        group_Explosion_Rounds = new GameObject("ExplosionRoundsPool");
        group_text_Damage = new GameObject("TextDamagePool");

        //creating object pool of ammo game object
        for (int i = 0; i <= pooled_Amount; i++)
        {
            GameObject O = Instantiate(bullet_Player);
            GameObject E = Instantiate(bullet_Enemy);
            GameObject C = Instantiate(bullet_Catridge);
            GameObject MS = Instantiate(misc_Spark);
            GameObject EHS = Instantiate(enemy_Hit_Spark);
            GameObject MFS = Instantiate(muzzle_Flash_Spark);
            GameObject EU = Instantiate(Explosion_Universal);
            GameObject EN = Instantiate(Explosion_Normal);
            GameObject ER = Instantiate(Explosion_Rounds);
            GameObject TD = Instantiate(text_Damage);

            bullet_Player_Pool.Add(O);
            bullet_Enemy_Pool.Add(E);
            bullet_Catridge_Pool.Add(C);
            misc_Spark_Pool.Add(MS);
            enemy_Hit_Spark_Pool.Add(EHS);
            muzzle_Flash_Spark_Pool.Add(MFS);
            Explosion_Universal_Pool.Add(EU);
            Explosion_Normal_Pool.Add(EN);
            Explosion_Rounds_Pool.Add(ER);
            text_Damage_Pool.Add(TD);

            O.transform.parent = group_Player_Bullet.transform;
            O.SetActive(false);
            E.transform.parent = group_Enemy_Bullet.transform;
            E.SetActive(false);
            C.transform.parent = group_Catridge_Bullet.transform;
            C.SetActive(false);
            MS.transform.parent = group_misc_Spark.transform;
            MS.SetActive(false);
            EHS.transform.parent = group_enemy_Hit_Spark.transform;
            EHS.SetActive(false);
            MFS.transform.parent = group_muzzle_Flash_Spark.transform;
            MFS.SetActive(false);
            EU.transform.parent = group_Explosion_Universal.transform;
            EU.SetActive(false);
            EN.transform.parent = group_Explosion_Normal.transform;
            EN.SetActive(false);
            ER.transform.parent = group_Explosion_Rounds.transform;
            ER.SetActive(false);
            TD.transform.parent = group_text_Damage.transform;
            TD.SetActive(false);
        }
        GameObject.DontDestroyOnLoad(group_Player_Bullet);
        GameObject.DontDestroyOnLoad(group_Enemy_Bullet);
        GameObject.DontDestroyOnLoad(group_Catridge_Bullet);
        GameObject.DontDestroyOnLoad(group_misc_Spark);
        GameObject.DontDestroyOnLoad(group_enemy_Hit_Spark);
        GameObject.DontDestroyOnLoad(group_muzzle_Flash_Spark);
        GameObject.DontDestroyOnLoad(group_Explosion_Universal);
        GameObject.DontDestroyOnLoad(group_Explosion_Normal);
        GameObject.DontDestroyOnLoad(group_Explosion_Rounds);
        GameObject.DontDestroyOnLoad(group_text_Damage);

    }
    public void DestroyAmmoPool()
    {
        for (int i = 0; i <= pooled_Amount; i++)
        {
            Destroy(bullet_Player_Pool[i]);
            Destroy(bullet_Enemy_Pool[i]);
            Destroy(bullet_Catridge_Pool[i]);
            Destroy(misc_Spark_Pool[i]);
            Destroy(enemy_Hit_Spark_Pool[i]);
            Destroy(muzzle_Flash_Spark_Pool[i]);
            Destroy(Explosion_Universal_Pool[i]);
            Destroy(Explosion_Normal_Pool[i]);
            Destroy(Explosion_Rounds_Pool[i]);
            Destroy(text_Damage_Pool[i]);
        }
    }
}
