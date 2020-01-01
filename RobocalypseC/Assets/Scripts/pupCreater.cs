
using UnityEngine;

public class pupCreater : MonoBehaviour
{
    public int ammo,id;
    public bool health, shield;
    public float healthAmount, shieldAmount;
    public GameObject primaryCreate, SecondaryCreate,GrenadeCreate,healthPUP,shieldPUP;
    public bool createOnStart=false;
    public void Start()
    {
        if (createOnStart) {
            Create();
        }
    }
    public void Create()
    {
        if (!health && !shield)
        {
            int type = id / 100;
            if (type == 2)
            {
                primary();
            }
            else if (type == 1)
            {
                secondary();
            }
            else if (type == 3) {
                grenades();
            }
        }
        if (health) {
            healthPUP.GetComponent<HelthPower>().health = healthAmount;
            Instantiate(healthPUP, transform.position, transform.rotation);
        }
        if (shield) {
            shieldPUP.GetComponent<ShieldPowerup>().shield = shieldAmount;
            Instantiate(shieldPUP, transform.position, transform.rotation);
        }
            Destroy(gameObject);
    }
    void grenades() {
        PickupsGrenades gk = GrenadeCreate.GetComponent<PickupsGrenades>();
        gk.id = id;
        gk.count = ammo;
        GameObject PUP= Instantiate(GrenadeCreate, transform.position, transform.rotation);
        
    }
    void primary() {
        PickupsPrimary pk= primaryCreate.GetComponent<PickupsPrimary>();
        pk.id = id;
        pk.ammo = ammo;
        GameObject PUP= Instantiate(primaryCreate, transform.position, transform.rotation);
        int type = (id / 10) % 10;
    }
    void secondary() {
        PickupsSecondary pk = SecondaryCreate.GetComponent<PickupsSecondary>();
        pk.id = id;
        pk.ammo = ammo;
        Instantiate(SecondaryCreate, transform.position, transform.rotation);
    }
}
