using UnityEngine;
public class lift : Switchable
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        IndicatorSetValue(0);
    }
    public override void Switch()
    {
        base.Switch();
        IndicatorSetValue(status ? 1 : 0);
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
