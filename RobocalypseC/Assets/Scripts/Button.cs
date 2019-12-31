using UnityEngine;
public class Button :Interactable
{

    public Switchable[] switchables;
    public override void Start()
    {
        base.Start();
    }
    override    
    public void InRange()
    {
    }
    override
    public void interact()
    {
        pressed();
    }
    override
    public void OutOfRange()
    {
        Debug.Log("out of range");
    }

    void pressed()
    {
        Debug.Log("pressed");
        foreach (Switchable s in switchables) {
            s.Switch();
        }
    }

}
