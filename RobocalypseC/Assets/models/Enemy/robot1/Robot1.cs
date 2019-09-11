
using UnityEngine;
public class Robot1 : AIObjects_Ranged,IKillable
{
private GunPlaceHolder gunPlaceHolder;
    [Header("Status")]
    public bool found;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        gunPlaceHolder = GetComponentInChildren<GunPlaceHolder>();

    }
    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void move()
    {
        base.move();
    }
    public override void Attack()
    {
        //pointAtPlayer
        Anim.SetFloat("lookDirection", Mathf.Lerp(Anim.GetFloat("lookDirection"), TargetAngle(transform.position,player.transform.position) + targetOffset - (targetOffset * (DistanceBetweenPlayerAndThis - attackRange) / DistanceBetweenPlayerAndThis), targetSmoother));
        is_walking = false;
        gunPlaceHolder.shoot();
    }
    public override void Jump()
    {
        if (GroundedCheck()&&is_walking)
        {
            Debug.Log("called");
            initialVelocity = jumpForce;
            Anim.SetTrigger("Jump");
            DontCheckGround = true;
        }
        base.Jump();
    }
    public override bool Heading()
    {
        return movingPositive;
       
    }

    public void dead()
    {
        throw new System.NotImplementedException();
    }
}
