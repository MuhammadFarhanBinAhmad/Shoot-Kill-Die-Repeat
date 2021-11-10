using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Rigidbody the_RB;
    CharacterController the_CC;
    CameraBob the_CB;
    //Basic
    public BasicCharacterDataStats the_Basic_Stats;
    public float speed_Movement;
    public float health_Player;
    public float health_Player_Current;
    public static int scrap_Total = 10; 
    public static int money_Total = 10000;
    //Jumping
    Vector3 velocity;
    public float gravity = -9.81f;
    public float jump_Force = 9.81f;
    public Transform check_Ground;
    public float ground_Distance = 0.5f;
    public LayerMask ground_Layer;
    //bool is_Grounded;
    public int number_of_Jumps;
    public BaseGunV2 the_BGV2;
    //PlayerUI
    PlayerUIHUD the_Player_UI_HUD;
    public GameObject press_E;
    //Effect
    internal float speed_Debuff_Time, sight_Debuff_Time, fire_Debuff_Time;

    //Others
    public GUNINATORGunCreation the_GUNINATOR;
    public ATM the_ATM;

    internal bool is_Store_Open;

    private void Start()
    {
        the_Player_UI_HUD = FindObjectOfType<PlayerUIHUD>();
        the_CC = GetComponent<CharacterController>();
        the_CB = GetComponent<CameraBob>();
        the_BGV2 = FindObjectOfType<BaseGunV2>();
        //set up character stats
        health_Player = the_Basic_Stats.health;
        health_Player_Current = health_Player;
        speed_Movement = the_Basic_Stats.speed;
        //current_Stamina = total_Stamina;

        //SwitchWeapon(current_Weapon);//equip 1st weapon at start
    }

    public void Update()
    {
        MovePlayer();
        //JUMP//
        if (Input.GetButtonDown("Jump") && number_of_Jumps < 1)
        {
            JumpPlayer();
        }
        //SWITCH WEAPON//
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            the_BGV2.current_Weapon_Equipped = 0;
            the_Player_UI_HUD.AmmoUpdateV2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            the_BGV2.current_Weapon_Equipped = 1;
            the_Player_UI_HUD.AmmoUpdateV2();
        }
        //ACCESS STORE//

        if (speed_Debuff_Time > 0)
        {
            speed_Debuff_Time -= Time.deltaTime;
            SpeedDebuff();
        }
        else
        {
            speed_Movement = the_Basic_Stats.speed;
        }

        /*if (fire_Debuff_Time > 0)
        {
            fire_Debuff_Time -= Time.deltaTime;
            FireDebuff();
        }*/
    }

    void MovePlayer()
    {

        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        //Movement
        Vector3 move = transform.right * H + transform.forward * V;
        the_CC.Move(move * speed_Movement * Time.deltaTime);

        if (H != 0 || V != 0)
        {
            the_CB.isWalking = true;
        }
        else
        {
            the_CB.isWalking = false;
        }
        //Gravity
        velocity.y += gravity * Time.deltaTime;
        the_CC.Move(velocity * Time.deltaTime);

        if (isGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    //check if player grounded
    internal bool isGrounded()
    {
        if (Physics.CheckSphere(check_Ground.position, ground_Distance, ground_Layer))
        {
            number_of_Jumps = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    void JumpPlayer()
    {
        velocity.y = Mathf.Sqrt(jump_Force * -2 * gravity);
        number_of_Jumps++;
    }
    void SwitchWeaponV2()
    {

    }

    internal void TakeDamage(float Dmg)
    {
        health_Player_Current -= Dmg;
        the_Player_UI_HUD.HealthUpdate();
        if (health_Player_Current <= 0)
        {
            the_Player_UI_HUD.GameOver();
            print("Dead");
        }
    }
    void SpeedDebuff()
    {
        speed_Movement = the_Basic_Stats.speed/2;
        print("Sticky");
    }
    /*void SightDebuff()
    {

    }
    void FireDebuff()
    {

    }*/
    public static void ResetPlayerData()
    {
        money_Total = 5;
    }
    //player able to pick up weapon
    private void OnTriggerEnter(Collider other)
    {
        //player enter GUN-inator premise
        if (other.GetComponent<GUNINATORGunCreation>() != null)
        {
            the_GUNINATOR = other.GetComponent<GUNINATORGunCreation>();
            press_E.SetActive(true);
        }
        //player enter ATM premise
        if (other.GetComponent<ATM>() != null)
        {
            the_ATM = other.GetComponent<ATM>();
            press_E.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //player leaves GUN-inator premise
        if (the_GUNINATOR !=null)
        {
            the_GUNINATOR = null;
            press_E.SetActive(false);
        }
        //player enter ATM premise
        if (the_ATM != null)
        {
            the_ATM = null;
            press_E.SetActive(false);
        }
    }
}
