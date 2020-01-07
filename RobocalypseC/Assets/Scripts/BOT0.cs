
using UnityEngine;

public class BOT0 : AIObjects,IKillable
{
    public GameObject gun;
    public float aimOffset;
    public GameObject cracked;
    public override void Attack()
    {
        base.Attack();
        point();
        shoot();
    }

    public void dead()
    {
        Instantiate(cracked, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void point() {
        Vector3 aim = new Vector3(player.transform.position.x, player.transform.position.y + aimOffset, player.transform.position.z);
        gun.transform.LookAt(aim);
    }
    void shoot() {
        gun.GetComponent<gun>().shoot(false);
    }
}
