using System.Collections;
using UnityEngine;

public class MechBoss : AIObjects_Ranged, IKillable
{
    private AudioSource audioPlayer;
    public AudioClip messileAudio;
    public AudioClip bulletAudio;
    public AudioClip eBulletAudio;
    public Animator anim;
    [Range(0, 1)]
    public float rightRotation;
    [Range(0, 1)]
    public float leftRotation;
    [Header("hand and guns")]
    public Transform gunL;
    public Transform gunR;
    [Range(0, 1)]
    public float messileProbability;
    [Header("status")]
    public bool active;
    private bool AIStarted = false;
    public int firemode;//0-default 1-explosive 2-messile
    [Header("bullet")]
    public Transform bulletTip;
    public GameObject bullet;
    public float bulletFireRate;
    public float bulletStateTime;
    public float bulletDamage,bulletHealthDamageRatio,bulletShieldDamageRatio;
    [Header ("explosive")]
    public Transform explosiveBulletTip;
    public GameObject explosiveBullet;
    public float explosiveBulletFireRate;
    public float explosiveBulletStateTime;
    public float explosiveBulletDamage,EBulletHealthDamageRatio,EBulletShieldDamageRatio;
    [Header("messile")]
    private int MessileState;//0-aim 1-shoot 3-exit
    public float messileAimTime, MessileShootTime;
    private float bulletTriggerCounter = 0;
    public Transform messileTip;
    public GameObject messile;
    public float messileDamage,messileHealthDamageRatio,messileShieldDamageRatio;
    public float messileFireRate;
    public float messileCorrection;
    public float messileRandomOffset;
    public float messileAimAngle;
    public GameObject DeadMechBoss;
    public override void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        base.Start();

    }
    void Update() {
        if (AIStarted && firemode != 2)
        {
            DummyChaseAtPlayer();
        }
        if (firemode == 2)
        {
            
            switch (MessileState)
            {
                case 0:
                    AimMessile(messileAimAngle);
                    break;
                case 1:
                    fireMessile();
                    break;
                case 2:
                    ChooseAction();
                    break;
            }
        }
        if (!AIStarted && Vector3.Distance(player.transform.position, transform.position) < detectRange) {
            MechBossAIStart();
        }
    }
    void fireMessile()
    {
        if (bulletTriggerCounter <= 0)
        {
            audioPlayer.clip = messileAudio;
            audioPlayer.Play();
            GameObject m= Instantiate(messile, messileTip.transform.position,transform.rotation);
            parabolicBullet pb = m.GetComponent<parabolicBullet>();
            pb.damage = messileDamage;
            pb.healthDamageRatio = messileHealthDamageRatio;
            pb.ShieldDamageRatio = messileShieldDamageRatio;
            pb.distance = Mathf.Abs( DistanceBetweenPlayerAndThis+Random.Range(-messileRandomOffset,messileRandomOffset)+messileCorrection);
            pb.gravity = gameManager.Gravity;
            pb.FromPlayer = false;
            bulletTriggerCounter = 1 / messileFireRate;
        }
        else
        {
            bulletTriggerCounter -= Time.deltaTime;
        }
    }
    public override void FixedUpdate()
    {
        if (is_alive)
        {
            DistanceBetweenPlayerAndThis = Vector3.Distance(player.transform.position, transform.position);
            transform.position.Set(0, transform.position.y, transform.position.z);
        }
        gravity();
        if (AIStarted && firemode == 2)
        {
            turnAtPlayer();
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
            case 2:
                break;
            default:
                DefaultBullet();
                break;
        } 
    }
    void PointGunRToPlayer() {
        float angle = TargetAngle(gunR.position, player.transform.position);
        anim.SetFloat("gunR", Mathf.Lerp(anim.GetFloat("gunR"), angle, targetSmoother * Time.deltaTime));
        AimMessile(0.5f);
    }
    void PointGunLToPlayer() {
        float angle = TargetAngle(gunL.position, player.transform.position);
        anim.SetFloat("gunL", Mathf.Lerp(anim.GetFloat("gunL"),angle,targetSmoother*Time.deltaTime));
        AimMessile(0.5f);
    }
    void AimMessile (float dir){
        anim.SetFloat("handR", Mathf.Lerp(anim.GetFloat("handR"), dir, targetSmoother * Time.deltaTime));
        anim.SetFloat("handL", Mathf.Lerp(anim.GetFloat("handL"), dir, targetSmoother * Time.deltaTime));
    }
    void DefaultBullet() {
        if (bulletTriggerCounter <= 0)
        {
            audioPlayer.clip = bulletAudio;
            audioPlayer.Play();
            Bullet b= Instantiate(bullet, bulletTip.transform.position,bulletTip.transform.rotation).GetComponent<Bullet>();
            b.FromPlayer=false;
            b.damage = bulletDamage;
            b.healthDamageRatio = bulletHealthDamageRatio;
            b.ShieldDamageRatio = bulletShieldDamageRatio;
            bulletTriggerCounter = 1 / bulletFireRate;
        }
        else
        {
            bulletTriggerCounter -= Time.deltaTime;
        }
        PointGunLToPlayer();
    }//point to player and shoot
    void ExplosiveBullet() {
        if (bulletTriggerCounter <= 0)
        {
            audioPlayer.clip = eBulletAudio;
            audioPlayer.Play();
            Bullet b= Instantiate(explosiveBullet, explosiveBulletTip.transform.position,explosiveBulletTip.transform.rotation).GetComponent<Bullet>();
            b.FromPlayer = false;
            b.damage = explosiveBulletDamage;
            b.healthDamageRatio = EBulletHealthDamageRatio;
            b.ShieldDamageRatio = EBulletShieldDamageRatio;
            bulletTriggerCounter = 1 / explosiveBulletFireRate;
        }
        else {
            bulletTriggerCounter -= Time.deltaTime;
        }
        PointGunRToPlayer();
    }//point to player and shoot
    IEnumerator Messiles() {
        MessileState = 0;
        anim.SetBool("isWalking", false);
        RestrictMoving = true;
        yield return new WaitForSeconds(messileAimTime);
        MessileState = 1;
        yield return new WaitForSeconds(MessileShootTime);
        MessileState = 2;
        RestrictMoving = false;
    }
    private void MechBossAIStart() {
        ChooseAction();
        AIStarted = true;
    }
    private void ChooseAction() {
        if (Random.Range(0f, 1f) < messileProbability||DistanceBetweenPlayerAndThis>detectRange)
        {
            firemode = 2;
            StartCoroutine(Messiles());
        }
        else {
            firemode = Random.Range(0, 2);
            StartCoroutine(StateTimer(firemode==0?bulletStateTime:explosiveBulletStateTime));
        }
    }
    private IEnumerator StateTimer(float time) {
        Debug.Log("hoose");
        yield return new WaitForSeconds(time);
        ChooseAction();
    }
    public void dead(){
        Instantiate(DeadMechBoss, transform.position, transform.rotation);
        is_alive = false;
        Collider[] col = GetComponents<Collider>();
        foreach (Collider collider in col)
        {
            collider.enabled = false;
        }
        Destroy(gameObject);
    }
}
