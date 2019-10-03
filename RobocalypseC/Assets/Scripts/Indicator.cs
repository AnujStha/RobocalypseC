
using UnityEngine;

public class Indicator : MonoBehaviour
{
    protected int status;
    private void Start()
    {
        ValueChanged();
    }
    public void SetValue(int value) {
        status = value;
        ValueChanged();
    }
    public virtual void ValueChanged()
    {
        
    }
}
