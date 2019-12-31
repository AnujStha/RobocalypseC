using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class IngameInfo : MonoBehaviour
{
    public TextMeshProUGUI health, shield, weapon, ammo;
    private GameObject player;
    private GunPlaceHolderPlayer gunPlaceHolderReference;
    private HealthAndShield healthAndShieldReference;
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
    }
    void healthAndShieldUpdate() {
        string text = "health:"+healthAndShieldReference.health.ToString() + "/" + healthAndShieldReference.MaxHealth.ToString();
        health.text = text;
        text = "Shield:"+healthAndShieldReference.Shield.ToString() + "/" + healthAndShieldReference.MaxShield.ToString();
        shield.text = text;
    }
    void ammoUpdate() {
        gun g = gunPlaceHolderReference.ActiveGun.GetComponent<gun>();
        string text = "ammo:" + g.ammoInMag+"/" +g.ammoCapacityMag+"   ammoInBag:"+g.ammoInBag;
        ammo.text = text;
    }
    void weaponUpdate() {
        int activeGrenadeIndex = gunPlaceHolderReference.ActiveGrenade;
        string text = "Gun:" + gunPlaceHolderReference.ActiveGun.GetComponent<gun>().id + "Grenade" + gunPlaceHolderReference.ActiveGrenade;
    }
}
