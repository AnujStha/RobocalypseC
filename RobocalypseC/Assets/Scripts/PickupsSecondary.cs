
using UnityEngine;

public class PickupsSecondary : Interactable
{
    public int id;
    public int ammo;
    public GameObject PUPanimation;
    public pupCreater create;
    public override void Start()
    {
        PUPanimation = GameObject.Find("GameManager").GetComponent<gameManager>().SecondaryPowerups[id % 10];
        base.Start();
        Instantiate(PUPanimation, transform);
        Color c = Color.black;
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
        GunPlaceHolderPlayer holder = gameManager.player.GetComponentInChildren<GunPlaceHolderPlayer>();
        bool replacing;
        if (holder.activeSecondary == id)
        {
            replacing = false;
        }
        else
        {
            replacing = true;
            ///
            create.transform.position = transform.position;
            gun g = holder.findWeaponByID(holder.activeSecondary).GetComponent<gun>();
            create.id = g.id;
            create.ammo = g.ammoInBag + g.ammoInMag;
            g.ammoInMag = 0;
        }
        holder.activeSecondary = id;
        if (!holder.holdingPrimary)
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
            create.Create();
        }
        else {
            holder.ActiveGun.GetComponent<gun>().ammoInBag += ammo;
        }
        Destroy(gameObject);
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
    public override void InRange()
    {
        base.InRange();
        GunPlaceHolderPlayer holder = gameManager.player.GetComponentInChildren<GunPlaceHolderPlayer>();
        if (holder.activeSecondary == id) {
            holder.findWeaponByID(id).GetComponent<gun>().ammoInBag += ammo;
            Destroy(gameObject);
        }
    }
}
