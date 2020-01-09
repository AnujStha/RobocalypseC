using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOTsaw : AIObjects,IKillable{
    public GameObject destroyObject;
    public GameObject wheel;
    public float wheelRotateSpeed;
    public float damagePS,healthDamageRatio,shieldDamageRatio;
    public override void Attack()
    {
        gameManager.player.GetComponent<HealthAndShield>().damage(damagePS*Time.fixedDeltaTime,healthDamageRatio,shieldDamageRatio);
    }
    void IKillable.dead() {
        Instantiate(destroyObject, transform.position, transform.rotation);
        Drop();
        Destroy(gameObject);
            

    }
    public override void move()
    {
        wheel.transform.Rotate(new Vector3(1, 0, 0), wheelRotateSpeed * Time.fixedDeltaTime);
        base.move();

    }
}
