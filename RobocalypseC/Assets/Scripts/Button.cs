using UnityEngine;
public class Button :Interactable
{

    public Switchable[] switchables;
    override    
    public void InRange()
    {
        Debug.Log("in range   ");
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
