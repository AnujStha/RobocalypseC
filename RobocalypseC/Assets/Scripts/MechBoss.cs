using System.Collections;
using UnityEngine;

public class MechBoss : AIObjects_Ranged,IKillable
{
    public Animator anim;
    [Range(0, 1)]
    public float rightRotation;
    [Range(0, 1)]
    public float leftRotation;
    [Header("hand and guns")]
    public Transform handL;
    public Transform handR;
    public Transform gunL;
    public Transform gunR;
    [Header("Bullets")]
    public Transform explosiveBulletTip;
    public GameObject explosiveBullet;
    public float explosiveBulletFireRate;
    public float explosiveBulletStateTime;
    public Transform bulletTip;
    public GameObject bullet;
    public float bulletFireRate;
    public float bulletStateTime;
    public Transform messileTip;
    public GameObject messile;
    public float messileFireRate;
    [Range(0,1)]
    public float messileProbability;
    [Header("status")]
    public bool active;
    private bool AIStarted=false;
    private bool ActionRunning = false;
    public int firemode;//0-default 1-explosive 2-messile
    private float bulletTriggerCounter = 0;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }
    public override void FixedUpdate()
    {
        DistanceBetweenPlayerAndThis = Vector3.Distance(player.transform.position, transform.position);
        if (ActionRunning && firemode != 2)
        {
            DummyChaseAtPlayer();
        }
    }

    public override void Attack()
    {
        is_walking = false;
        switch (firemode) {
            case 0:
                DefaultBullet();
                break;
            case 1:
                ExplosiveBullet();
                break;
            default:
                DefaultBullet();
                break;
        } 
    }
    void PointGunRToPlayer() {
        float angle = TargetAngle(gunR.position, player.transform.position);
        anim.SetFloat("gunR", Mathf.Lerp(anim.GetFloat("gunR"), angle, targetSmoother * Time.deltaTime));
    }
    void PointGunLToPlayer() {
        float angle = TargetAngle(gunL.position, player.transform.position);
        anim.SetFloat("gunL", Mathf.Lerp(anim.GetFloat("gunL"),angle,targetSmoother*Time.deltaTime));
    }
    void AimMessile (){

    }
    void DefaultBullet() {
        if (bulletTriggerCounter <= 0)
        {
            Instantiate(bullet, bulletTip.transform.position,bulletTip.transform.rotation);
            bulletTriggerCounter = 1 / bulletFireRate;
        }
        else
        {
            bulletTriggerCounter -= Time.deltaTime;
        }
    }
    void ExplosiveBullet() {
        if (bulletTriggerCounter <= 0)
        {
            Instantiate(explosiveBullet, explosiveBulletTip.transform.position,explosiveBulletTip.transform.rotation);
            bulletTriggerCounter = 1 / explosiveBulletFireRate;
        }
        else {
            bulletTriggerCounter -= Time.deltaTime;
        }
    }
    void Messiles() {
        Debug.Log("messiles");
        ActionRunning = false;
    }
    private void MechBossAIStart() {
        ChooseAction();
        
    }
    private void ChooseAction() {
        Debug.Log("action");
        if (Random.Range(0f, 1f) < messileProbability)
        {
            Messiles();
        }
        else {
            firemode = Random.Range(0, 2);
            StartCoroutine(StateTimer(firemode==0?bulletStateTime:explosiveBulletStateTime));
        }
    }
    private IEnumerator StateTimer(float time) {
        yield return new WaitForSeconds(time);
        ActionRunning = false;
        bulletTriggerCounter = 0;
    }

    public void dead()
    {
        throw new System.NotImplementedException();
    }
}
