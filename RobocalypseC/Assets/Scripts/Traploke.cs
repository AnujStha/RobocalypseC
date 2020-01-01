
using System.Collections;
using UnityEngine;

public class Traploke : AIObjects,IKillable
{
    public bool attacking;
    public float attack1Time;
    public float attack2Time;
    public float attack1HitTime, attack2HitTime,attack1Range,attack2Range;
    public float attack1Damage, attack2Damage, healthDamageRatio, shieldDamageRatio;
    [Range(0,1)]
    public float attack2Probability;
    public override void Start()
    {
        base.Start();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void Attack()
    {
        if (!attacking)
        {
            if (Random.Range(0f, 1f) < attack2Probability)
            {
                Anim.SetTrigger("attack2");
                StartCoroutine(HaltMovement(attack2Time));
                StartCoroutine(Damage(attack1HitTime, attack1Damage, attack1Range));
            }
            else
            {
                Anim.SetTrigger("attack1");
                StartCoroutine(HaltMovement(attack1Time));
                StartCoroutine(Damage(attack2HitTime, attack2Damage, attack2Range));
            }
        }
    }
    IEnumerator HaltMovement(float time)
    {
        RestrictMoving = true;
        attacking = true;
        yield return new WaitForSeconds(time);
        RestrictMoving = false;
        attacking = false;
    }
    IEnumerator Damage(float time, float damage,float range)
    {

        yield return new WaitForSeconds(time);
        if (DistanceBetweenPlayerAndThis < range)
        {
            player.GetComponent<HealthAndShield>().damage(damage, healthDamageRatio, shieldDamageRatio);
        }
    }
    public void dead()
    {
        Anim.SetTrigger("death");
        is_alive = false;
        Collider[] col= GetComponents<Collider>();
        foreach (Collider collider in col) {
            collider.enabled = false;
        }
    }
}
