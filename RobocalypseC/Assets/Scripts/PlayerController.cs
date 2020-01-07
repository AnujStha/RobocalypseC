using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IKillable
{
    private GunPlaceHolder gunHolder;
    private Animator Anim;
    [Range(0, 1)]
    [Header("Movement")]
    public float moveDirection;
    public float WalkSpeed;
    public bool isAlive=true;
    public float RunSpeed;
    private float Speed;
    private float inAirtime = 0;
    public float Gravity;
    public float jumpForce;
    float initialVelocity = 0;
    private float verticalVelocity;
    [Header("Status")]
    public bool running;
    public bool is_inAir;
    public bool movingForward;
    public bool isShooting;
    [Header("Sensors")]
    public bool facingPositive;
    public float overheadClearance;
    public float GroundClearance;
    public Transform groundCheckPoint;
    public GameObject piviot;
    private CharacterController Controller;
    public Vector3 boxsize;
    public float jumpMemoryTime;
    private float jumpMemoryCounter;
    [Header("others")]
    private List<int> keys = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        gunHolder = GetComponentInChildren<GunPlaceHolder>();
        Gravity = gameManager.Gravity;
    }
    // Update is called once per frame
    void Update()
    {
        if (!gameManager.Paused&&isAlive)
        {
            move();
            point_at_mouse();
            //Debug.Log(verticalVelocity+" "+inAirtime);
            gameObject.transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }
    void move() {
        Anim = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
        //for jump,falling etc related to y axis
        if (jumpMemoryCounter > 0) {
            jumpMemoryCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpMemoryCounter = jumpMemoryTime;
        }
        if (GroundedCheck())
        {
            initialVelocity = 0;
            if (jumpMemoryCounter>0) {
                // Debug.Log("jump");
                jumpMemoryCounter = 0;
                initialVelocity = jumpForce;
                Anim.SetTrigger("Jump");
            }
            inAirtime = 0;
            is_inAir = false;

        }
        else
        {
            if (verticalVelocity > 0)
            {
                if (overhead_check())
                {
                    inAirtime = 0;
                    initialVelocity = 0;
                }
            }
            inAirtime += Time.deltaTime;
            is_inAir = true;
        }
        verticalVelocity = initialVelocity - Gravity * inAirtime;
        moveDirection = Input.GetAxis("Horizontal");
        Vector3 Direction = new Vector3(0, verticalVelocity, moveDirection * Speed) * Time.deltaTime;
        Controller.Move(Direction);
        //walking direction
        if ((moveDirection > 0 && facingPositive) || (moveDirection < 0 && !facingPositive))
        {
            movingForward = true;
        }
        else if ((moveDirection < 0 && facingPositive) || (moveDirection > 0 && !facingPositive))
        {
            movingForward = false;
            running = false;
        }

        //animation state
        int animationState;
        //Debug.Log(gunHolder.isShooting);
        if (movingForward && isShooting == false)
        {
            running = true;
        }
        else {
            running = false;
        }
        if (moveDirection == 0)
        {
            Speed = WalkSpeed;
            animationState = 1;
        }
        else if (movingForward == false)
        {
            Speed = WalkSpeed;
            animationState = 0;
            running = false;
        }
        else if (running == false)
        {
            Speed = WalkSpeed;
            animationState = 2;
        }
        else {
            animationState = 4;
            Speed = RunSpeed;
        }

        //movement animation
        float animationBlend = animationState;
        animationBlend /= 3;
        //Debug.Log(animationBlend);
        Anim.SetFloat("Speed", animationBlend);
    }
    bool overhead_check() {
        RaycastHit hit;
        bool isStruck=false;
        if (Physics.Raycast(piviot.transform.position, Vector3.up, out hit, overheadClearance))
        {
            if(!hit.collider.isTrigger)
            isStruck = true;
        }
        else {
            isStruck = false;
        }
        return isStruck;

       
    }
    bool GroundedCheck()
    {
        RaycastHit hit;
        bool isGrounded=false;
        if (Physics.BoxCast(groundCheckPoint.position, boxsize, Vector3.down,out hit,Quaternion.identity,GroundClearance)){
            isGrounded = true;
        }
        Anim.SetBool("Grounded", isGrounded);
        return isGrounded;
    }
    void point_at_mouse()
    {   
        Vector2 mouseposition = Input.mousePosition;
        Vector2 handScreenPosition = Camera.main.WorldToScreenPoint(piviot.transform.position);

        float angle = Vector2.Angle(mouseposition - handScreenPosition, new Vector2(1, 0));
        //Debug.Log(mouseposition + " " + handScreenPosition);
        //remapping angle to 0 to 1 from -90 to +90
        if (mouseposition.x > handScreenPosition.x)
        {
            facingPositive = true;
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(transform.rotation.x, 0, transform.rotation.z)));
            if (mouseposition.y < handScreenPosition.y)
            {
                angle *= -1f;
            }
            angle += 90;
            angle /= 180;
        }
        else
        {
            facingPositive = false;
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(transform.rotation.x, 180, transform.rotation.z)));
            angle -= 90;
            angle /= 90;
            angle = 1 - angle;
            if (mouseposition.y < handScreenPosition.y)
            {
                angle *= -1f;
            }
            angle /= 2;
            angle += 0.5f;
        }
        //Debug.Log(angle);
        Anim.SetFloat("lookDirection", angle);

        //Debug.Log (dir.x);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawRay(piviot.transform.position + new Vector3(0, overheadClearance, 0), Vector3.up);
        Gizmos.DrawCube(groundCheckPoint.position,boxsize);
    }
    public void AddKey(int key) {
        keys.Add(key);
        Debug.Log("key:" + key + " added");
    }//add key to player
    public bool CheckKey(int key) {
        bool found=false;
        foreach (int k in keys) {
            if (key == k) found = true;
        }
        return found;
    }//check if key is with player
    public void dead()
    {
        Anim.SetTrigger("dead");
        Anim.SetBool("isDead", true);
        isAlive = false;
    }
}
