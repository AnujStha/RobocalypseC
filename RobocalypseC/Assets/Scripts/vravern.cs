using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vravern : AIObjects,IKillable
{
    public float attack1Time;
    public bool attacking=false;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void Attack()
    {
        if (attacking == false)
        {
            Anim.SetTrigger("attack1");
            StartCoroutine(HaltMovement(attack1Time));
        }
    }
    
    IEnumerator HaltMovement(float time) {
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
