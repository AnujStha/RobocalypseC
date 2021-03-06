﻿using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class IngameInfo : MonoBehaviour
{
    public Image health, shield, ammo, reloadBar;
    public float healthMaxUI, healthMinUI, shieldMaxUI, shieldMinUI;
    public GameObject reloading, noAmmo;
    public TextMeshProUGUI ammoInBag,grenadeCount;
    private GameObject player;
    private GunPlaceHolderPlayer gunPlaceHolderReference;
    private HealthAndShield healthAndShieldReference;
    public GameObject[] primaryIcon;
    public GameObject[] secondaryIcon;
    public GameObject[] grenadeIcons;
    private int primaryIconIndex=0,secondaryIconIndex=0,grenadeIconIndex=0;
    // Start is called before the first frame update
    void Start()
    {
        player = gameManager.player;
        gunPlaceHolderReference = player.GetComponentInChildren<GunPlaceHolderPlayer>();
        healthAndShieldReference = player.GetComponent<HealthAndShield>();
    }
    // Update is called once per frame
    void Update()
    {
        healthAndShieldUpdate();
        ammoUpdate();
        weaponUpdate();
        reloadingAndNoAmmo();
    }
    void healthAndShieldUpdate() {/*
        string text = "health:"+healthAndShieldReference.health.ToString() + "/" + healthAndShieldReference.MaxHealth.ToString();
        health.text = text;
        text = "Shield:"+healthAndShieldReference.Shield.ToString() + "/" + healthAndShieldReference.MaxShield.ToString();
        shield.text = text;*/
        float healthAmount = healthAndShieldReference.health / healthAndShieldReference.MaxHealth;
        health.fillAmount = ((healthMaxUI - healthMinUI) * healthAmount) + healthMinUI;
        float shieldAmount = healthAndShieldReference.Shield / healthAndShieldReference.MaxShield;
        shield.fillAmount = ((shieldMaxUI - shieldMinUI) * shieldAmount) + shieldMinUI;
    }
    void ammoUpdate() {/*
        gun g = gunPlaceHolderReference.ActiveGun.GetComponent<gun>();
        string text = "ammo:" + g.ammoInMag+"/" +g.ammoCapacityMag+"   ammoInBag:"+g.ammoInBag;
        ammo.text = text;
        */
        gun g = gunPlaceHolderReference.ActiveGun.GetComponent<gun>();
        float ammoAmount = g.ammoInMag*1f / (g.ammoCapacityMag);
        ammo.fillAmount = ammoAmount;
        ammoInBag.text = g.ammoInBag.ToString();
        int activeGrenadeIndex = gunPlaceHolderReference.ActiveGrenade;
        activeGrenadeIndex %= 10;
        int GrenadeNumber=gunPlaceHolderReference.grenadeCount[activeGrenadeIndex];
        grenadeCount.text = GrenadeNumber.ToString();
    }
    void weaponUpdate() {/*
        int activeGrenadeIndex = gunPlaceHolderReference.ActiveGrenade;
        string text = "Gun:" + gunPlaceHolderReference.ActiveGun.GetComponent<gun>().id + "Grenade" + gunPlaceHolderReference.ActiveGrenade + ":" + gunPlaceHolderReference.grenadeCount[activeGrenadeIndex%100];
        weapon.text = text;
        */
        int activePrimary = gunPlaceHolderReference.activePrimary;
        int activeSecondary = gunPlaceHolderReference.activeSecondary;
        int activeGrenade = gunPlaceHolderReference.ActiveGrenade;
        ////////
        ///
        int primaryEffect = (activePrimary % 100) / 10;
        int primaryGun = activePrimary % 10;
        if (primaryIconIndex != primaryGun * 3 + primaryEffect) {
            primaryIcon[primaryIconIndex].SetActive(false);
            primaryIcon[primaryGun * 3 + primaryEffect].SetActive(true);
            primaryIconIndex = primaryGun * 3 + primaryEffect;
        }
        int secondaryEffect = (activeSecondary % 100) / 10;
        int secondaryGun = activeSecondary % 10;
        if (secondaryIconIndex != secondaryGun * 3 + secondaryEffect)
        {
            secondaryIcon[secondaryIconIndex].SetActive(false);
            secondaryIcon[secondaryGun * 3 + secondaryEffect].SetActive(true);
            secondaryIconIndex = secondaryGun * 3 + secondaryEffect;
        }
        int grenadeIndex = activeGrenade % 10;
        if (grenadeIconIndex != grenadeIndex) {
            grenadeIcons[grenadeIconIndex].SetActive(false);
            grenadeIcons[grenadeIndex].SetActive(true);
            grenadeIconIndex = grenadeIndex;
        }
    }
    void reloadingAndNoAmmo() {/*
        gun g= gunPlaceHolderReference.ActiveGun.GetComponent<gun>();
        if (g.reloading) {
            float reloadPercent = (g.ReloadCount / g.reloadTime) * 100;
            ammo.text = ammo.text + "   reloading:" + reloadPercent.ToString("F0") + "%";
        }
        if (g.noAmmo) {
            ammo.text = ammo.text + "NoAmmo";
        }
        */
        gun g = gunPlaceHolderReference.ActiveGun.GetComponent<gun>();
        if (g.reloading)
        {
            float reloadPercent = (g.ReloadCount / g.reloadTime);
            reloadBar.fillAmount =1- reloadPercent;
            reloading.SetActive(true);
        }
        else {
            reloading.SetActive(false);
        }
        if (g.noAmmo)
        {
            noAmmo.SetActive(true);
        }
        else {
            noAmmo.SetActive(false);
        }
    }
}
