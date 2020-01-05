using UnityEngine;

public class VravenWithKeyDrop : vravern
{
    public GameObject key;
    public override void dead()
    {
        Instantiate(key, transform.position, transform.rotation);
        base.dead();


    }
}
