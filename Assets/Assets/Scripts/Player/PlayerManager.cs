using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Rigidbody the_RB;
    CharacterController the_CC;
    CameraBob the_CB;
    PlayerCamera the_PC;
    [Header("Health")]
    public BasicCharacterDataStats the_Basic_Stats;
    public float health_Player;
    public float health_Player_Current;
    [Header("Money")]
    public static int money_Total = 100;
    [Header("Movements")]
    //Speed
    public float speed_Movement;
    Vector3 velocity;
    //Jumping
    public float gravity = -9.81f;
    public float jump_Force = 9.81f;
    public Transform check_Ground;
    public float ground_Distance = 0.5f;
    public LayerMask ground_Layer;
    //Dashing
    public float DashSpeed;
    //bool is_Grounded;
    [SerializeField]
    int additional_Jumps;
    public int number_of_Jumps;
    public BaseGunV2 the_BGV2;
    //PlayerUI
    PlayerUIHUD the_Player_UI_HUD;
    public GameObject press_E;
    //Debuff Effect
    internal float speed_Debuff_Time, sight_Debuff_Time, fire_Debuff_Time;
    public float speed_Collectables_Magnet;
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
        if (Input.GetButtonDown("Jump") && number_of_Jumps <= additional_Jumps)
        {
            JumpPlayer();
        }
        //SWITCH WEAPON//
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            the_BGV2.current_Weapon_Equipped = 0;
            the_BGV2.ChangeWeaponModel();
            the_Player_UI_HUD.AmmoUpdateV2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            if (the_BGV2.current_WM_Installed[1] !=null)
            {
                the_BGV2.current_Weapon_Equipped = 1;
                the_BGV2.ChangeWeaponModel();
                the_Player_UI_HUD.AmmoUpdateV2();
            }
        }
    }
    void MovePlayer()
    {

        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        //Movement
        Vector3 move = transform.right * H + transform.forward * V;
        the_CC.Move(move * speed_Movement * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayerDashing(move);
        }
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
    void PlayerDashing(Vector3 move)
    {
        the_CC.Move(move * DashSpeed * Time.deltaTime);
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
    internal void TakeDamage(float Dmg)
    {
        health_Player_Current -= Dmg;

        the_Player_UI_HUD.HealthUpdate();
        the_Player_UI_HUD.DamageUI();

        if (health_Player_Current <= 0)
        {
            the_Player_UI_HUD.GameOver();
            print("Dead");
        }
    }
    /// <Taking Fire Damage Effect>
    internal IEnumerator FireDamageEffect()
    {
        InvokeRepeating("TakingFireDamage", 0, 1);
        yield return new WaitForSeconds(5);
        StopFireDamage(); ;
    }
    void TakingFireDamage()
    {
        TakeDamage(2);
        print("TakingFireDmg");
    }
    void StopFireDamage()
    {
        CancelInvoke();
        print("StopFireDamage");
    }
    /// <Taking Speed Debug Effect>
    internal IEnumerator SpeedDebuffEffect()
    {
        SpeedDebuff();
        yield return new WaitForSeconds(2);
        speed_Movement = the_Basic_Stats.speed;

    }
    void SpeedDebuff()
    {
        speed_Movement = the_Basic_Stats.speed/2;
        print("Sticky");
    }
    /*void SightDebuff()
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
