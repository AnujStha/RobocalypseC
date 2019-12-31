//Author @ ANUJ SHRESTHA
using UnityEngine;

public class Grenades : MonoBehaviour {
	public int grenadeID;
	protected Rigidbody grenadeRB;
	public float timer;
	public GameObject ExplosionEffect;
	public bool isExploded;
	public float Grenade_Force_min;
	public float Grenade_force_max;
	public float range;
	public GameObject GrenadeRenderer;
	public float damage;
	public float GrenadeAimTime;//0-1
    public float grenadeShieldDamageRatio;
    public float grenadeHealthDamageRatio;
	public float grenadeForce;
	[Header("audio")]
	private AudioSource audioSource;
	public virtual void Start () {
		grenadeRB = gameObject.GetComponent<Rigidbody > ();
		Vector3 grenadeDirection = new Vector3 (0,0,Mathf.Lerp(Grenade_Force_min,Grenade_force_max,GrenadeAimTime) );
		grenadeDirection = gameObject.transform.rotation*grenadeDirection;
		grenadeRB.AddForce (grenadeDirection, ForceMode.Impulse);
		grenadeRB.AddTorque(new Vector3(Random.Range(0,10),Random.Range(0,10),Random.Range(0,10)));
		isExploded = false;
		audioSource = GetComponent<AudioSource> ();
	}
	public virtual void Update () {

		timer -= Time.deltaTime;
		if (timer < 0&&isExploded==false)  explode ();
		
	}
	public virtual void explode(){
		
		isExploded = true;
		GrenadeDamage ();
		grenadeRB.isKinematic = true;
		GrenadeRenderer.SetActive (false);
		GameObject explosionEffect= Instantiate (ExplosionEffect, transform.position, transform.rotation);
		Destroy (explosionEffect, 2);
		Destroy (gameObject);
	}
	void GrenadeDamage(){
		Collider []colliders=Physics.OverlapSphere(transform.position,range);
		foreach (Collider col in colliders){
			Rigidbody victimRB = col.gameObject.GetComponent <Rigidbody> ();
			if (victimRB != null&&victimRB.gameObject!=gameObject)
				Force (victimRB);
			if (col.CompareTag("Player")||col.CompareTag("Enemy")||col.CompareTag("Destroyable")) {
                HealthAndShield VictimHealth = col.gameObject.GetComponent<HealthAndShield>();
                float distance = Vector3.Distance (col.gameObject.transform.position, transform.position);
				if(distance<range)
				VictimHealth.damage( damage -  (distance/range) * damage,grenadeHealthDamageRatio,grenadeShieldDamageRatio) ;
			
			}
		}

	}
	void Force(Rigidbody rb){
		Vector3 vect = rb.gameObject.transform.position - gameObject.transform.position;
		Vector3 direction = vect / vect.magnitude;

		rb.AddForce(direction*(1-vect.magnitude/range)*grenadeForce,ForceMode.Impulse);

	}
	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position,range);


	}
	void OnCollisionEnter(){
		audioSource.Play ();
	}
}
