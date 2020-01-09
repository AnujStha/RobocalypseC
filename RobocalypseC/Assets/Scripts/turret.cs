//author @ ANUJ SHRESTHA
//uses shoot dumb AI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour,IKillable {
	public GameObject gun;
	public GameObject bullet;
	public float shootRange;
	public bool playerAtRange;
	public bool playerVisible;
	private GameObject player; 
	public GameObject tip;
	public float burstGap;
	public float gapBetweenBurst;
	public int noOfShotInBurst;
	private float shotTimer;
	public  float damage,healthDamageRatio,shieldDamageRatio;
	public GameObject DestroyEffect;
	private AudioSource audioSource;
    public float targetOffset;
	// Use this for initialization
	void Start () {
		shotTimer = 0;
        player = gameManager.player;
		audioSource = GetComponentInChildren<AudioSource> ();
	}
	// Update is called once per frame
	void Update () {
		sensors ();
		target ();
	}
	void target(){
        if (playerAtRange)
        {
            Vector3 dir = new Vector3(player.transform.position.x, player.transform.position.y + targetOffset, player.transform.position.z);
            gun.transform.LookAt(dir);
        }
		if (playerAtRange && playerVisible) {
			shoot ();
		} 
	}
	void sensors(){
		if (Vector3.Distance (player.transform.position, gameObject.transform.position) < shootRange) {
			playerAtRange = true;
		} else {
			playerAtRange = false;
		}
			RaycastHit hit;
        Vector3 offsetDir = new Vector3(player.transform.position.x, player.transform.position.y + targetOffset, player.transform.position.z);
			Vector3 dir =offsetDir - tip.transform.position;
			Physics.Raycast (tip.transform.position, dir, out hit, 500);
		if (hit.collider != null) {
			
			if (hit.collider.tag == "Player") {
				//line below solves error( NullReferenceException)
				playerVisible = true;
			
			} else {
				playerVisible = false;
			}
		}

		

	}
	void shoot(){
		if (shotTimer < 0) {
			StartCoroutine (burst());
			shotTimer = gapBetweenBurst;
		} else {
			shotTimer -= Time.deltaTime;
		}
	}
	IEnumerator burst(){
		for(int i=0;i<noOfShotInBurst;i++){
			GameObject Bul=Instantiate (bullet, tip.transform.position, tip.transform.rotation);
			audioSource.Stop ();
			audioSource.Play ();
			Bullet bulletScript = Bul.GetComponent<Bullet> ();
			bulletScript.FromPlayer = false;
			bulletScript.damage = damage;
            bulletScript.healthDamageRatio = healthDamageRatio;
            bulletScript.ShieldDamageRatio = shieldDamageRatio;
			yield return new WaitForSeconds (burstGap);
			}
		}
	void OnDrawGizmosSelected()
	{
		// Display the radius when selected
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, shootRange);
	}
    void IKillable.dead()
    {
        GameObject cracked = Instantiate(DestroyEffect, transform.position, transform.rotation);
        Rigidbody[] rb = cracked.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody r in rb)
        {
            r.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
        Destroy(cracked, 4);
        Destroy(gameObject);
    }
}
