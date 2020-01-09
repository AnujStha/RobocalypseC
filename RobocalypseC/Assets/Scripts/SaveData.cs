
using UnityEngine;
[System.Serializable]
public class SaveData
{
    public float[] playerPosition;
    public int primaryGunID,secondaryGunID,PrimaryAmmoInBag,PrimaryAmmoInMag,SecondaryAmmoInBag,SecondaryAmmoInMag,level;
    public int[] GrenadeCount;
    public float health, shield;
    public SaveData(GameObject player, GunPlaceHolderPlayer guns,HealthAndShield healthAndShield,int levelThis) {
        level = levelThis;
        playerPosition = new float[3]; 
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;
        primaryGunID = guns.activePrimary;
        GrenadeCount = new int[3];
        secondaryGunID = guns.activeSecondary;
        GrenadeCount[0] = guns.grenadeCount[0];
        GrenadeCount[1] = guns.grenadeCount[1];
        GrenadeCount[2] = guns.grenadeCount[2];
        health = healthAndShield.health;
        shield = healthAndShield.Shield;
        gun PrimaryGun = guns.findWeaponByID(guns.activePrimary).GetComponent<gun>();
        PrimaryAmmoInBag = PrimaryGun.ammoInBag;
        PrimaryAmmoInMag = PrimaryGun.ammoInMag;
        gun SecondaryGun = guns.findWeaponByID(guns.activeSecondary).GetComponent<gun>();
        SecondaryAmmoInBag = SecondaryGun.ammoInBag;
        SecondaryAmmoInMag = SecondaryGun.ammoInMag;
    }
}
