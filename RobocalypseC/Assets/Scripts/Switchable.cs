
using UnityEngine;
public class Switchable : MonoBehaviour
{
    private bool status;
   public virtual void Switch() {
        Debug.Log("switch" + "  " + gameObject.name);
        if (status) off();
        else On();
    }
    public virtual void On() {
        status = true;
        Debug.Log("on" + "  " + gameObject.name);
    }
    public virtual void off() {
        Debug.Log("off" + "  " + gameObject.name);
        status = false;
    }
}
