using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class HealthAndShield : MonoBehaviour
{
    public float health;
    public float MaxHealth;
    public float Shield;
    public float MaxShield;
    public bool IsAlive;
    public float shieldRechargeRate;
    public float healthRechargeRate;
    public float healthRechargeDelay;
    public float shieldRechargeDelay;
    public bool HealthRechargeActive;
    public bool ShieldRechargeActive;
    [Header("UI")]
    public bool healthBarPresent;
    public float depletionShowTime;
    public Image healthBar,shieldBar,healthBarDepletionIndicator,ShieldbarDepletionIndicator;
    public float depletonDecreaseSmoothRate;
    private bool healthDepletionMeterFollow=true;
    private float shieldDepletionReference, HealthDepletionReference;
    [Header("FlyingText")]
    public GameObject FlyingText;
    public Color shieldDepletionColour, healthDepletionColour;

    // Start is called before the first frame update
    void Start()
    {
        healthDepletionMeterFollow = true;
        HealthDepletionReference = MaxHealth;
        shieldDepletionReference = MaxShield;
    }

    private void Awake()
    {
        HealthRechargeActive = true;
        ShieldRechargeActive = true;
        IsAlive = true;
        health = MaxHealth;
        Shield = MaxShield;
    }

    void FixedUpdate()
    {
        if (healthBarPresent)
        {
            healthBar.fillAmount = health / MaxHealth;
            shieldBar.fillAmount = Shield / MaxShield;
            healthBarDepletionIndicator.fillAmount = HealthDepletionReference/MaxHealth;
            ShieldbarDepletionIndicator.fillAmount = shieldDepletionReference/MaxShield;
            if (healthDepletionMeterFollow)
            {
                HealthDepletionReference = Mathf.Lerp(HealthDepletionReference, health, depletonDecreaseSmoothRate * Time.fixedDeltaTime);
                shieldDepletionReference = Mathf.Lerp(shieldDepletionReference, Shield, depletonDecreaseSmoothRate * Time.fixedDeltaTime);
            }
        }
        if (IsAlive&&(health<MaxHealth||Shield<MaxShield)) { 
        recharge(healthRechargeRate * Time.fixedDeltaTime*(HealthRechargeActive?1:0), shieldRechargeRate * Time.fixedDeltaTime*(ShieldRechargeActive?1:0));
        }

    }

    public void damage(float damage, float healthDamageRatio, float ShieldDamageRatio) {
        if (healthDepletionMeterFollow&&healthBarPresent) { 
        StartCoroutine(showReference());
    }
        if (Shield > damage * ShieldDamageRatio)
        {
            Shield -= damage * ShieldDamageRatio;
            StartCoroutine(ShieldRechargeDelayCounter());
        }
        else {

            damage -= Shield / ShieldDamageRatio;
            Shield = 0;
            if (health > damage * healthDamageRatio)
            {
                
                health -= damage * healthDamageRatio;
            }
            else {
                health = 0;
                IsAlive = false;
            }
            StartCoroutine(HealthRechargeDelayCounter());
        }


    }

    public void recharge(float healthRecharge,float shieldRecharge) {
        if (health < MaxHealth - healthRecharge)
        {
            health += healthRecharge;
        }
        else {
            health = MaxHealth;
        }
        if (Shield < MaxShield - shieldRecharge)
        {
            Shield += shieldRecharge;
        }
        else
        {
            Shield = MaxShield;
        }
    }


    IEnumerator ShieldRechargeDelayCounter() {
        ShieldRechargeActive = false;
        yield return new WaitForSeconds(shieldRechargeDelay);
        ShieldRechargeActive = true;
    }

    IEnumerator HealthRechargeDelayCounter()
    {
        HealthRechargeActive = false;
        yield return new WaitForSeconds(healthRechargeDelay);
        HealthRechargeActive = true;
    }

    IEnumerator showReference() {
        healthDepletionMeterFollow = false;
        yield return new WaitForSeconds(depletionShowTime);
        healthDepletionMeterFollow = true;

    }
}
