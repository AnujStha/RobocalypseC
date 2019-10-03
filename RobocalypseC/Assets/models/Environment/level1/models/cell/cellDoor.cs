
using UnityEngine;
public class cellDoor : Switchable
{
    private bool opened;
    private void Start()
    {
        IndicatorSetValue(0);
    }
    public override void On()
    {
        if (!opened)
        {
            opened = true;
            GetComponent<Animation>().Play();
            IndicatorSetValue(1);
        }
        base.On();

    }
}
