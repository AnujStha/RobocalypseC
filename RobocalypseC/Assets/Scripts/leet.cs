using System.Collections;
using UnityEngine;

public class leet : AIObjects,IKillable
{

    public bool attacking;
    public float attack1Time;
    public float attack2Time;
    [Range(0, 1)]
    public float attack2Probability;


    // Start is called before the first frame update
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
            }
            else
            {
                Anim.SetTrigger("attack1");
                StartCoroutine(HaltMovement(attack1Time));
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
    public void dead()
    {
        Anim.SetTrigger("death");
        is_alive = false;
        Collider[] col = GetComponents<Collider>();
        foreach (Collider collider in col)
        {
            collider.enabled = false;
        }
    }
}
