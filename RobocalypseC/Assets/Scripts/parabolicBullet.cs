using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parabolicBullet : Bullet
{
    public float distance;
    public float gravity;
    private float velocityX, velocityY;
    public override void Start()
    {
        bulletSpeed = Mathf.Sqrt(distance * gravity / 2);
        velocityY = bulletSpeed;
        velocityX = bulletSpeed;
        Destroy(gameObject, BulletLife);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocityY -= gravity * Time.fixedDeltaTime;
        transform.Translate(0,velocityY * Time.fixedDeltaTime, velocityX * Time.fixedDeltaTime);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
