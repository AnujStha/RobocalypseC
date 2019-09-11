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
            Grenades G = Instantiate(Grenades[ActiveGrenade], tip.position,tip.rotation).GetComponent<Grenades>();
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

    void switchWeapon(){
        if (holdingPrimary)
        {
            ActiveGun.GetComponent<gun>().activate(false);
            ActiveGun = secondaryGuns[activeSecondary];
            ActiveGun.GetComponent<gun>().activate(true);
            firemode = ActiveGun.GetComponent<gun>().firemode;
            holdingPrimary = false;
        }
        else {
            ActiveGun.GetComponent<gun>().activate(false);
            ActiveGun = primaryGuns[activeSecondary];
            ActiveGun.GetComponent<gun>().activate(true);
            firemode = ActiveGun.GetComponent<gun>().firemode;
            holdingPrimary = true;
        }
    }

}
