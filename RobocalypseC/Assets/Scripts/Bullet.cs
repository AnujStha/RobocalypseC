//Author @ ANUJ SHRESTHA


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float bulletSpeed;
	public float damage;
    public float BulletLife;
    public bool FromPlayer;
    public float healthDamageRatio;
    public float ShieldDamageRatio;




	public virtual void Start () {

        Rigidbody bulletRB;
        bulletRB = gameObject.GetComponent<Rigidbody> ();
		Vector3 bulletDirection = new Vector3 (0,0,bulletSpeed);
		bulletDirection = gameObject.transform.rotation*bulletDirection;
		bulletRB.AddForce (bulletDirection, ForceMode.Force);
        Destroy(gameObject, BulletLife);
	}
    void OnTriggerEnter(Collider col)
    {
        if (!col.isTrigger) { 
        if ((col.gameObject.CompareTag("Enemy") && FromPlayer) || (col.gameObject.CompareTag("Player") && !FromPlayer) || col.gameObject.CompareTag("Destroyable"))
        {
            col.gameObject.GetComponent<HealthAndShield>().damage(damage, healthDamageRatio, ShieldDamageRatio);
        }
        destroy();
    }
	}

    public void destroy()
    {
        Destroy(gameObject);
    }

    
}
