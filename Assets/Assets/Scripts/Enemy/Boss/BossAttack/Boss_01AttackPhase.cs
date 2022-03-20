using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_01AttackPhase : MonoBehaviour
{
    [SerializeField] int total_AttackType;
    int attacktype_Number;
    int current_AttackType;

    [HeaderAttribute("LaserAttack")]
    [SerializeField] GameObject Laser;
    [SerializeField] GameObject LaserRim;
    [SerializeField] float laser_Speed_Rotation;
    [SerializeField] float laserphase_Up_Time;
    [HeaderAttribute("LavaAttackPhase")]
    public GameObject LavaAttackGameObject;
    public GameObject Force_Fields;
    [SerializeField] float lava_Speed_Rotation;
    [SerializeField] float lavaattackphase_Up_Time;
    [HeaderAttribute("Cannon")]
    public List<Cannon> all_Canon = new List<Cannon>();
    [SerializeField] float cannonphase_Up_Time;
    [HeaderAttribute("FireOrb")]
    [SerializeField] float FireOrb_Up_Time;

    private void Start()
    {
        StartCoroutine(Resting());
    }

    void AttackType()
    {
        attacktype_Number = Random.Range(1,3);
        switch (attacktype_Number)
        {
            case 1:
                StartCoroutine(LaserAttack());
                break;
            case 2:
                StartCoroutine(LavaFloorAttack());
                break;
            case 3:
                StartCoroutine(CannonAttack());
                break;
            case 4:
                StartCoroutine(FireOrbAttack());
                break;
        }
    }
    void AttackingPhase()
    {
        switch (current_AttackType)
        {
            case 1:
                {
                    Laser.SetActive(true);
                    LaserRim.transform.Rotate(0, laser_Speed_Rotation, 0);
                    break;
                }
            case 2:
                {
                    LavaAttackGameObject.SetActive(true);
                    Force_Fields.transform.Rotate(lava_Speed_Rotation, 0, 0);
                    break;
                }
            case 3:
                {
                    for (int i = 0; i <= all_Canon.Count - 1; i++)
                    {
                        all_Canon[i].FireCannon();
                    }
                    break;
                }
        }

    }
    IEnumerator LaserAttack()
    {
        InvokeRepeating("AttackingPhase", 0, .03f);
        current_AttackType = 1;
        yield return new WaitForSeconds(laserphase_Up_Time);
        Laser.SetActive(false);
        StartCoroutine(Resting());
    }
    IEnumerator LavaFloorAttack()
    {
        InvokeRepeating("AttackingPhase", 0, .03f);
        current_AttackType = 2;
        yield return new WaitForSeconds(lavaattackphase_Up_Time);
        LavaAttackGameObject.SetActive(false);
        StartCoroutine(Resting());
    }
    IEnumerator CannonAttack()
    {
        InvokeRepeating("AttackingPhase", 0, 1);
        current_AttackType = 3;
        yield return new WaitForSeconds(cannonphase_Up_Time);
        StartCoroutine(Resting());
    }
    IEnumerator FireOrbAttack()
    {
        yield return new WaitForSeconds(FireOrb_Up_Time);
    }
    IEnumerator Resting()
    {
        CancelInvoke();
        yield return new WaitForSeconds(3);
        AttackType();
    }
}
