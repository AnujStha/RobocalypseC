using UnityEngine;

public class ButtonStandLights : Indicator
{
    public GameObject greenLight, redLight;
    public override void ValueChanged()
    {
        Debug.Log(status+"asd");
        if (status == 1) GreenLight();
        else RedLight();
    }
    void RedLight() {
        greenLight.SetActive(false);
        redLight.SetActive(true);
    }
    void GreenLight() {
        greenLight.SetActive(true);
        redLight.SetActive(false);
    }
}
