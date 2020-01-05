
using UnityEngine;

public class AIObjects : MonoBehaviour
{
    public Transform piviot;
    public Animator Anim;
    public float Speed;
    public float jumpForce;
    public float rotationSmoother;
    [Header("Sensors")]
    public float detectRange;
    public float attackRange;
    public float overheadClearance;
    public float GroundClearance;
    [Header("status")]
    public bool is_inAir;
    protected float DistanceBetweenPlayerAndThis;
    protected GameObject player;
    public bool is_walking;
    public bool is_alive = true;
    protected bool RestrictMoving=false;

    protected float initialVelocity;
    protected float inAirTime;
    protected float verticalVelocity;
    protected float Gravity;
    protected bool DontCheckGround = false;
    private CharacterController Controller;
    public bool movingPositive;
    [Header("drop")]
    public GameObject drop;
    [Range(0, 1)]
    public float healthAndShieldDropChance;
    [Range(0, 1)]
    public float WeaponDropChance;
    [Range(0, 1)]
    public float UsingWeaponDropChance;

    public virtual void Start()
    {
        player = gameManager.player;
        Gravity = gameManager.Gravity;
        Anim = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
    }
    public virtual void FixedUpdate()
    {
        if (is_alive)
        {
            DistanceBetweenPlayerAndThis = Vector3.Distance(player.transform.position, transform.position);
            DummyChaseAtPlayer();
            transform.position=new Vector3(0, transform.position.y, transform.position.z);
        }
        gravity();
    }
    protected void DummyChaseAtPlayer() {
       
        if (DistanceBetweenPlayerAndThis < detectRange)
        {
            turnAtPlayer();
          
            if (DistanceBetweenPlayerAndThis < attackRange)
            {
                Attack();
                is_walking = false;
                Anim.SetBool("isWalking", false);
            }
            else
            {
                move();
            }
        }
        else
        {
            is_walking = false;
            Anim.SetBool("isWalking", false);
        }
    } 
    public virtual void Attack() {

    }
    public virtual bool Heading() {
        return true;
    }
    protected bool Overhead_check()
    {
        RaycastHit hit;
        bool isStruck = false;
        if (Physics.Raycast(piviot.transform.position, Vector3.up, out hit, overheadClearance))
        {
            if (!hit.collider.isTrigger)
                isStruck = true;
        }
        else
        {
            isStruck = false;
        }
        return isStruck;


    }
    protected bool GroundedCheck()
    {
        RaycastHit hit;
        bool isGrounded = false;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, GroundClearance))
        {
            if (!hit.collider.isTrigger)
                isGrounded = true;
        }

        //Anim.SetBool("inAir", !isGrounded);
        return isGrounded;
    }
    protected void gravity()
    {
        if (GroundedCheck() && !DontCheckGround)
        {
            initialVelocity = 0;
            inAirTime = 0;
            is_inAir = false;
        }
        else
        {
            if (verticalVelocity > 0)
            {
                if (Overhead_check())
                {
                    inAirTime = 0;
                    initialVelocity = 0;
                }
            }
            inAirTime += Time.fixedDeltaTime;
            is_inAir = true;
        }
        verticalVelocity = initialVelocity - Gravity * inAirTime;
    }
    public virtual void Jump() {

    }
    public virtual void move()
    {
            Vector3 Direction = new Vector3(0, verticalVelocity, (movingPositive ? 1 : -1) * (RestrictMoving? 0 : 1) * Speed) * Time.deltaTime;
            Controller.Move(Direction);
            DontCheckGround = false;
            Anim.SetBool("isWalking", !RestrictMoving);
    }
    protected void turnAtPlayer() {
        FindPlayerSide();
        if (movingPositive)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z), rotationSmoother * Time.fixedDeltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z), rotationSmoother * Time.fixedDeltaTime);
        }
    }
    protected void FindPlayerSide() {
        if (player.transform.position.z > transform.position.z)
        {
            movingPositive = true;

        }
        else
        {
            movingPositive = false;

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(piviot.transform.position + new Vector3(0, overheadClearance, 0), Vector3.up);
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
    protected void Drop() {
        gameManager manager = GameObject.Find("GameManager").GetComponent<gameManager>();
        if (Random.Range(0f, 1f) < healthAndShieldDropChance) {
            GameObject hs=Instantiate(drop, transform.position, transform.rotation);
            pupCreater pup= hs.GetComponent<pupCreater>();
            if (Random.Range(0, 2) == 0)
            {
                pup.health = true;
                pup.healthAmount = manager.healthRecharge_;
            }
            else {
                pup.shield = true;
                pup.shieldAmount = manager.shieldRecharge_;
            }
            pup.Create();
        }
        float chance= Random.Range(0f, 1f);

        if (chance < WeaponDropChance) {
            Debug.Log(chance+"  "+WeaponDropChance);
            GameObject hs = Instantiate(drop, transform.position, transform.rotation);
            pupCreater pup = hs.GetComponent<pupCreater>();
            if (Random.Range(0f, 1f) < UsingWeaponDropChance)
            {
                if (Random.Range(0, 4) != 0)
                {
                    int Gunid = gameManager.player.GetComponentInChildren<GunPlaceHolderPlayer>().activePrimary;
                    pup.id = Gunid;
                    int type = Gunid % 10;
                    pup.ammo = manager.AmmoRechargePrimary[type];
                }
                else {
                    int type = Random.Range(0, 3);
                    int id = 300 + type;
                    int ammoFill = manager.rechargeGrenades[type];
                    pup.id = id;
                    pup.ammo = ammoFill;
                }
            }
            else {
                int choice = Random.Range(0,5);
                int variant = Random.Range(0, 3);
                int ammoFill=0;
                int type=0;
                int id=0;
                if (choice != 0) {
                    type = Random.Range(0, 5);
                    id = 200 + variant * 10 + type;
                    ammoFill = manager.AmmoRechargePrimary[type];
                }
                else { 
                        type = 0;
                        id = 100 + variant * 10 + type;
                        ammoFill = manager.ammorechargeSecondary[type];
                }
                pup.id = id;
                pup.ammo = ammoFill;
            }
            pup.Create();
        }
    }
}
