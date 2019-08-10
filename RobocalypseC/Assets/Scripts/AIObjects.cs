﻿
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
    public gameManager Manager;
    public bool is_walking;

    protected float initialVelocity;
    protected float inAirTime;
    protected float verticalVelocity;
    protected float Gravity;
    protected bool DontCheckGround = false;
    private CharacterController Controller;
    public bool movingPositive;

    public virtual void Start()
    {
        player = Manager.Player;
        Gravity = Manager.Gravity;
        Anim = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
    }
    public virtual void FixedUpdate()
    {
        move();
        turnAtPlayer();
        DistanceBetweenPlayerAndThis = Vector3.Distance(player.transform.position, transform.position);
        DummyChaseAtPlayer();
       
    }

    protected void DummyChaseAtPlayer() {
        if (DistanceBetweenPlayerAndThis < detectRange)
        {
            turnAtPlayer();
            if (DistanceBetweenPlayerAndThis < attackRange)
            {
                Attack();
            }
            else
            {
                Chase();
            }
        }
        else
        {
            is_walking = false;
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

        Anim.SetBool("inAir", !isGrounded);
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
    protected void Chase()
    {
        is_walking = true;
    }
    public virtual void move() {
        Vector3 Direction = new Vector3(0, verticalVelocity, (movingPositive ? 1 : -1) * (is_walking ? 1 : 0) * Speed) * Time.deltaTime;
        gravity();
        Controller.Move(Direction);
        DontCheckGround = false;
        Anim.SetBool("isWalking",is_walking);
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
}
