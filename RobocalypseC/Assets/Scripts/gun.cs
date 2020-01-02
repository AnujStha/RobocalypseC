using System.Collections;
using UnityEngine;

public class gun : MonoBehaviour {
	public int id;
	public Transform tip;
	public bool active;
	public GameObject Bullet;

	[Range(1,2)]
	public int firemode;
	public bool infiniteAmmo;
	public int ammoCapacityMag;
	public int ammoInMag;
	public int ammoInBag;
    [Header("gunStatus")]
    private bool FireReady;
    public float fireRate;
    [Range(0, 10)]
    public float accuracy;
    [Header("reload")]
	public float reloadTime;
	public float  ReloadCount;
	public bool reloading;
	public bool noAmmo;
	[Header("sounds")]
	public AudioClip[] bulletFireSound;
	public AudioClip emptyAudio;
	public AudioSource BulletAudioSource;
    [Header("Damage")]
    public float damage;
    public float healthDamageRatio;
    public float shieldDamageRatio;
	void Start(){
		//gameObject.SetActive (false);
		ammoInMag=ammoCapacityMag;
		reloading = false;
		ReloadCount = reloadTime;
        FireReady = true;
        BulletAudioSource = GetComponent<AudioSource>();
	}
	void Update(){
		
		if (reloading) {
			ReloadCount -= Time.deltaTime;
			if (ReloadCount < 0) {
			
				reload ();
			}
		}
	
	}//Update
    public void shoot(bool fromPlayer) {
        if (FireReady&&!reloading){
            if (ammoInMag > 0)
            {
                playAudio();
                //decrease bullet
                ammoInMag --;
                //add Bullet Property
                float deflect = Random.Range(10-accuracy, accuracy-10);
                Vector3 shootDirection = new Vector3(tip.transform.rotation.eulerAngles.x + deflect, tip.transform.rotation.eulerAngles.y, tip.transform.rotation.eulerAngles.z);
                GameObject bullet = Instantiate(Bullet, tip.transform.position, Quaternion.Euler(shootDirection));
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.damage = damage;
                bulletScript.healthDamageRatio = healthDamageRatio;
                bulletScript.ShieldDamageRatio = shieldDamageRatio;
                bulletScript.FromPlayer = fromPlayer;
                StartCoroutine(HaltFire(1 / fireRate));
            }
            else {
                PlayEmptyAudio();
                reload();
            }
        }
    }
    IEnumerator HaltFire(float time) {
        FireReady = false;
        yield return new WaitForSeconds(time);
        //Debug.Log("h");

        FireReady = true;
    }
	public void clear(){
		ammoInMag = ammoCapacityMag;

	
	}
	public void reload(){

        //UI
        //reloadingUI.Reloading = true;
        if (ammoInBag > 0)
        {
            if (ReloadCount > 0)
            {
                reloading = true;
            }
            else
            {
                reloading = false;
                //UI
                //reloadingUI.Reloading=false;
                //
                if (ammoInBag >= ammoCapacityMag - ammoInMag)
                {
                    ammoInBag -= ammoCapacityMag - ammoInMag;
                    ammoInMag = ammoCapacityMag;
                }
                else if (ammoInBag > 0)
                {
                    ammoInMag += ammoInBag;
                    ammoInBag = 0;

                }
                if (infiniteAmmo)
                {
                    ammoInBag += ammoCapacityMag;
                }
                ReloadCount = reloadTime;
            }
        }
        else {
            NoAmmo();
        }
	}
	public void haltReload(){
		reloading = false;
		ReloadCount = reloadTime;
	
	}
	public void activate(bool command){
        ReloadCount = 1;
        reloading = false;
		gameObject.SetActive (command);
		active = command;
        //because ienumerator doesnt activate if switched
        FireReady = true;

	}
	void NoAmmo(){
		noAmmo = true;
		//noAmmoUI.NoAmmo = true;

	}
	public void playAudio (){
		BulletAudioSource.Stop ();
		int randAudio=Random.Range(0,bulletFireSound.Length);
		BulletAudioSource.clip=bulletFireSound[randAudio];
		BulletAudioSource.Play ();
	}
    public void PlayEmptyAudio() {
        BulletAudioSource.Stop();
        BulletAudioSource.clip = emptyAudio;
        BulletAudioSource.Play();
    }
}
