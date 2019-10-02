
using UnityEngine;
public class cellDoor : Switchable
{
    private bool opened;
    public override void On()
    {
        Debug.Log("called");
        GetComponent<Animation>().Play();
        base.On();

    }
}
