using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlaceHolderPlayer : GunPlaceHolder
{

    public GameObject[] primaryGuns;
    public GameObject[] secondaryGuns;
    public GameObject[] Grenades;
    public int activePrimary, activeSecondary, ActiveGrenade;
    public bool holdingPrimary;
    private PlayerController playerSc;
    private int firemode;
    private float GrenadeHoldTime;
    public float grenadeMaxHoldTime;
    // Start is called before the first frame update
    public override void Start()
    {
        ActiveGun = findWeaponByID(activePrimary);
        ActiveGun.GetComponent<gun>().activate(true);
        base.Start();
        Player = true;
        playerSc = GetComponentInParent<PlayerController>();
        firemode = ActiveGun.GetComponent<gun>().firemode;
    }
        private void Update()
    {
        if (Input.GetButtonDown("Switch")) {
            switchWeapon ();
        }
        if (Input.GetButtonDown("Reload")) {
            ActiveGun.GetComponent<gun>().reload();
        }
        if (Input.GetButton("Grenade")) {
            if(GrenadeHoldTime<grenadeMaxHoldTime)
            GrenadeHoldTime += Time.deltaTime;
        }
        if (Input.GetButtonUp("Grenade"))
        {
            Transform tip = ActiveGun.GetComponent<gun>().tip.transform;
            Grenades G = Instantiate(findWeaponByID(ActiveGrenade), tip.position,tip.rotation).GetComponent<Grenades>();
            G.GrenadeAimTime = GrenadeHoldTime / grenadeMaxHoldTime;
            GrenadeHoldTime = 0;
        }
        switch (firemode) {
            case 1:
                if (Input.GetButton("Fire1"))
                {
                    shoot();
                    playerSc.isShooting = true;
                }
                else {
                    playerSc.isShooting = false;
                }
                break;

            case 2:
                if (Input.GetButtonDown("Fire1"))
                {
                    shoot();
                    playerSc.isShooting = true;
                }
                else
                {
                    playerSc.isShooting = false;
                }
                break;


        }
    }
    // Update is called once per frame
    public override void LateUpdate()
    {
        base.LateUpdate();
    }
    GameObject findWeaponByID(int id) {
        int type = id / 100;
        GameObject weapon=null;
        switch (type) {
            case 01:
                for(int i = 0; i < secondaryGuns.Length; i++)
                {
                    gun testGun = secondaryGuns[i].GetComponent<gun>();
                    if (testGun.id == id) {
                        weapon = testGun.gameObject;
                        break;
                    }
                }
                break;
            case 02:
                for (int i = 0; i < primaryGuns.Length; i++)
                {
                    gun testGun = primaryGuns[i].GetComponent<gun>();
                    if (testGun.id == id)
                    {
                        weapon = testGun.gameObject;
                        break;
                    }
                }
                break;
            case 03:
                for (int i = 0; i < Grenades.Length; i++)
                {
                    Grenades testGun = Grenades[i].GetComponent<Grenades>();
                    if (testGun.grenadeID == id)
                    {
                        weapon = testGun.gameObject;
                        break;
                    }
                }
                break;

        }

        return weapon;
    }
    void switchWeapon(){
        if (holdingPrimary)
        {
            ActiveGun.GetComponent<gun>().activate(false);
            ActiveGun = findWeaponByID(activePrimary);
            ActiveGun.GetComponent<gun>().activate(true);
            firemode = ActiveGun.GetComponent<gun>().firemode;
            holdingPrimary = false;
        }
        else {
            ActiveGun.GetComponent<gun>().activate(false);
            ActiveGun = findWeaponByID(activeSecondary);
            ActiveGun.GetComponent<gun>().activate(true);
            firemode = ActiveGun.GetComponent<gun>().firemode;
            holdingPrimary = true;
        }
    }

}
