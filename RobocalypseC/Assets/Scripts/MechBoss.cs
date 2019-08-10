
using UnityEngine;

public class MechBoss : AIObjects_Ranged
{
    public Animator anim;
    [Range(0,1)]
    public float rightRotation;
    [Range(0, 1)]
    public float leftRotation;

    [Header("Sensors")]
    public bool active;

    public float rotationsmoother;
    [Header("hand and guns")]
    public Transform handL;
    public Transform handR;
    public Transform gunL;
    public Transform gunR;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("handR", rightRotation);
        anim.SetFloat("handL", leftRotation);
        PointGunRToPlayer();
        PointGunLToPlayer();

        
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Attack()
    {
        is_walking = false;
    }
    void PointGunRToPlayer() {
        float angle = TargetAngle(gunR.position, player.transform.position);
        anim.SetFloat("gunR", Mathf.Lerp(anim.GetFloat("gunR"), angle, rotationsmoother * Time.deltaTime));
    }
    void PointGunLToPlayer() {
        float angle = TargetAngle(gunL.position, player.transform.position);
        anim.SetFloat("gunL", Mathf.Lerp(anim.GetFloat("gunL"),angle,rotationsmoother*Time.deltaTime));
    }
    void PointRightMessileToPlayer() {

    }
    void PointLeftMessileToPlayer() {

    }
    void Movement() {

    }
}
