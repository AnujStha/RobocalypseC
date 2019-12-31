
using UnityEngine;

public class key : Interactable
{
    public int keyCode;
    public override void Start()
    {
        base.Start();
    }
    public override void interact()
    {
        player.GetComponent<PlayerController>().AddKey(keyCode);
        base.interact();
        Destroy(gameObject);
    }
}
