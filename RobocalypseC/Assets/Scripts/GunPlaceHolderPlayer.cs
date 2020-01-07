using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlaceHolderPlayer : GunPlaceHolder
{

    public GameObject[] primaryGuns;
    public GameObject[] secondaryGuns;
    public GameObject[] Grenades;
    public int[] grenadeCount;
    public int activePrimary, activeSecondary, ActiveGrenade;
    public bool holdingPrimary;
    private PlayerController playerSc;
    private int firemode;
    private float GrenadeHoldTime;
    public float grenadeMaxHoldTime;
    public GameObject line;
    // Start is called before the first frame update
    public override void Start()
    {
        ActiveGun = findWeaponByID(activePrimary);
        ActiveGun.GetComponent<gun>().activate(true);
        base.Start();
        Player = true;
        playerSc = GetComponentInParent<PlayerController>();
        firemode = ActiveGun.GetComponent<gun>().firemode;
        SwitchGrenade();
    }
    private void Update()
    {
        if (playerSc.isAlive)
        {
            line.transform.position = ActiveGun.GetComponent<gun>().tip.position;
            if (Input.GetButtonDown("Switch"))
            {
                switchWeapon();
            }
            if (Input.GetButtonDown("Reload"))
            {
                ActiveGun.GetComponent<gun>().reload();
            }
            if (Input.GetButton("Grenade"))
            {
                if (GrenadeHoldTime < grenadeMaxHoldTime)
                    GrenadeHoldTime += Time.deltaTime;
            }
            if (Input.GetButtonUp("Grenade"))
            {
                Transform tip = ActiveGun.GetComponent<gun>().tip.transform;
                if (grenadeCount[ActiveGrenade % 300] > 0)
                {
                    Grenades G = Instantiate(findWeaponByID(ActiveGrenade), tip.position, tip.rotation).GetComponent<Grenades>();
                    G.GrenadeAimTime = GrenadeHoldTime / grenadeMaxHoldTime;
                    GrenadeHoldTime = 0;
                    grenadeCount[ActiveGrenade % 300]--;
                }
                if (grenadeCount[ActiveGrenade % 300] < 1)
                {
                    SwitchGrenade();
                }
            }
            switch (firemode)
            {
                case 1:
                    if (Input.GetButton("Fire1"))
                    {
                        shoot();
                        playerSc.isShooting = true;
                    }
                    else
                    {
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
            if (Input.GetButtonDown("SwitchGrenade"))
            {
                SwitchGrenade();
            }
        }
        }
    void SwitchGrenade() {
        int currentGrenade = ActiveGrenade;
        currentGrenade %= 300;
        for (int i = 1; i < Grenades.Length; i++) {
            if (grenadeCount[(currentGrenade + i) % Grenades.Length] > 0) {
                ActiveGrenade = 300 +( i + currentGrenade)%grenadeCount.Length;
                break;
            }
        }
    }
    // Update is called once per frame
    public override void LateUpdate()
    {
        base.LateUpdate();
    }
    public GameObject findWeaponByID(int id) {
        int type = id / 100;
        GameObject weapon = null;
        switch (type) {
            case 01:
                for (int i = 0; i < secondaryGuns.Length; i++)
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
    public void switchWeapon() {
        if (holdingPrimary)
        {
            ActiveGun.GetComponent<gun>().activate(false);
            ActiveGun = findWeaponByID(activeSecondary);
            ActiveGun.GetComponent<gun>().activate(true);
            firemode = ActiveGun.GetComponent<gun>().firemode;
            holdingPrimary = false;
        }
        else {
            ActiveGun.GetComponent<gun>().activate(false);
            ActiveGun = findWeaponByID(activePrimary);
            ActiveGun.GetComponent<gun>().activate(true);
            firemode = ActiveGun.GetComponent<gun>().firemode;
            holdingPrimary = true;
        }
    }

}