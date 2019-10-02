using UnityEngine;
public class lift : Switchable
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public override void Switch()
    {
        base.Switch();
    }
    public override void On()
    {
        anim.SetBool("status", true);
        base.On();
    }
    public override void off()
    {
        anim.SetBool("status", false);
        base.off();
    }
}
