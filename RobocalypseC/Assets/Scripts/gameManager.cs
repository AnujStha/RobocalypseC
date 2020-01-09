using UnityEngine.SceneManagement;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject Player;
    public static float Gravity;
    public static GameObject player;
    public Color shieldColour_, healthColour_, healthDepletion_, shieldDepletion_, poisionColour_;
    public static Color shieldColour, healthColour, healthDepletion, shieldDepletion, poisionColour;
    public float GravityC;
    public GameObject[] PrimaryPowerups;
    public GameObject[] SecondaryPowerups;
    public GameObject[] GrenadePowerups;
    public GameObject HealthPowerup;
    public GameObject ShieldPowerup;
    public float healthRecharge_, shieldRecharge_;
    public int[] AmmoRechargePrimary;
    public int[] ammorechargeSecondary;
    public int[] rechargeGrenades;
    public static bool Paused=false;
    private void Awake()
    {
        gameManager.Gravity = GravityC;
        gameManager.player = Player;
        gameManager.shieldColour = shieldColour_;
        gameManager.healthColour = healthColour_;
        gameManager.healthDepletion = healthDepletion_;
        gameManager.shieldDepletion = shieldDepletion_;
        gameManager.poisionColour = poisionColour_;

        
    }
    private void Start()
    {
        GameObject messenger = GameObject.Find("messenger");
        if (messenger != null)
        {
            if (messenger.GetComponent<Continue>().continueLevel)
            {
                loadScene();
            }
            Destroy(messenger);
        }
    }
    private void loadScene() {
        SaveData data = SaveSystem.loadPlayer();
        if (data.level == SceneManager.GetActiveScene().buildIndex)
        {
            Vector3 position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
            Player.transform.position = position;
            HealthAndShield healthAndShield = Player.GetComponent<HealthAndShield>();
            healthAndShield.health = data.health;
            healthAndShield.Shield = data.shield;
            GunPlaceHolderPlayer gunHolder = player.GetComponentInChildren<GunPlaceHolderPlayer>();
            gunHolder.activePrimary = data.primaryGunID;
            gun Primary = gunHolder.findWeaponByID(data.primaryGunID).GetComponent<gun>();
            Primary.ammoInBag = data.PrimaryAmmoInBag;
            Primary.ammoInMag = data.PrimaryAmmoInMag;
            gunHolder.activeSecondary = data.secondaryGunID;
            gun Secondary = gunHolder.findWeaponByID(data.secondaryGunID).GetComponent<gun>();
            Secondary.ammoInBag = data.SecondaryAmmoInBag;
            Secondary.ammoInMag = data.SecondaryAmmoInMag;
            gunHolder.grenadeCount[0] = data.GrenadeCount[0];
            gunHolder.grenadeCount[1] = data.GrenadeCount[1];
            gunHolder.grenadeCount[2] = data.GrenadeCount[2];
        }

    }
}
