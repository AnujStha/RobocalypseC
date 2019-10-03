
using UnityEngine;
public class Switchable : MonoBehaviour
{
    protected bool status;
    public Indicator[] Indicators;
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
    protected void IndicatorSetValue(int value) {
        foreach (Indicator i in Indicators)
        {
            i.SetValue(value);
        }
    }
}
