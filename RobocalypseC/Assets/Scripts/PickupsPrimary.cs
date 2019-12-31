﻿
using UnityEngine;

public class PickupsPrimary : Interactable
{
    public int id;
    public int ammo;
    private GameObject PUPanimation;
    public override void Start()
    {
        PUPanimation = GameObject.Find("GameManager").GetComponent<gameManager>().PrimaryPowerups[id%10];
        base.Start();
        Instantiate(PUPanimation, transform);
        Color c = Color.black ;
        int effect = (id % 100) / 10;
        if (effect == 0)
            c = Color.white;
        else if (effect == 1)
            c = gameManager.shieldDepletion;
        else if (effect == 2)
            c = gameManager.poisionColour;
        GetComponentInChildren<LineRenderer>().endColor = c;
    }
    public override void interact()
    {
        base.interact();
        int type = id / 100;
        GunPlaceHolderPlayer holder = gameManager.player.GetComponentInChildren<GunPlaceHolderPlayer>();
        bool replacing;
        if (holder.activePrimary == id)
        {
            replacing = false;
        }
        else {
            replacing = true;
        }
        holder.activePrimary = id;
        if (holder.holdingPrimary)
        {
            holder.switchWeapon();
            holder.switchWeapon();
        }
        else {
            holder.switchWeapon();
        }
        if (replacing)
        {
            holder.ActiveGun.GetComponent<gun>().ammoInBag = ammo;
        }
        else {
            holder.ActiveGun.GetComponent<gun>().ammoInBag += ammo;
        }
        Destroy(gameObject);
    }
    public override void InRange()
    {
        base.InRange();
        GunPlaceHolderPlayer holder = gameManager.player.GetComponentInChildren<GunPlaceHolderPlayer>();
        if (holder.activePrimary == id)
        {
            holder.findWeaponByID(id).GetComponent<gun>().ammoInBag += ammo;
            Destroy(gameObject);
        }
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
}
