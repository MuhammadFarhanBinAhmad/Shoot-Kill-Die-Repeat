using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossPhaseShield : MonoBehaviour
{
    [SerializeReference] GameObject ConnectedShield;

    [SerializeReference] int shield_Health;

    void CalculateDamage(BulletStats_ForPlayer BSFP)
    {
        int new_Bullet_Damage;
        //Damage value drop over time
        float P = (int)Mathf.Round((BSFP.bullet_Active_Up_Time / BSFP.bullet_Active_Time) * 100);//percent
        new_Bullet_Damage = (int)Mathf.Round((P / 100) * BSFP.bullet_Damage);
        TakingDamage(new_Bullet_Damage, this.gameObject);

        BSFP.Destroy();
    }
    internal void TakingDamage(int dmg, GameObject GO)
    {
        shield_Health -= dmg;

        //Damage text facing player
        Vector3 direction = FindObjectOfType<PlayerManager>().transform.position - transform.position;
        Quaternion current_Rotation = Quaternion.LookRotation(-direction);

        AmmoPool AP = FindObjectOfType<AmmoPool>();
        for (int i = 0; i < AP.text_Damage_Pool.Count; i++)
        {
            if (!AP.text_Damage_Pool[i].activeInHierarchy)
            {
                AP.text_Damage_Pool[i].transform.position = new Vector3(transform.position.x + (Random.Range(-.1f, .1f)), transform.position.y + .55f, transform.position.z + (Random.Range(-.1f, .1f)));
                AP.text_Damage_Pool[i].transform.rotation = current_Rotation;
                AP.text_Damage_Pool[i].GetComponentInChildren<TextMeshProUGUI>().text = dmg.ToString();
                AP.text_Damage_Pool[i].SetActive(true);
                break;
            }
        }

        if (shield_Health <= 0)
        {
            FindObjectOfType<EnemyBossManager>().boss_phase++;
            Destroy(ConnectedShield);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletStats_ForPlayer>() !=null)
        {
            CalculateDamage(other.GetComponent<BulletStats_ForPlayer>());
        }
    }
}
