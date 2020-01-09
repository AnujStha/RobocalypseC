using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMachine : MonoBehaviour
{
    public float spawnRange;
    public float detectRange;
    public GameObject drop;
    public float spawnInterval;
    public float healthAndShieldDropChance, WeaponDropChance, UsingWeaponDropChance,grenadeDropChance;
    private GameObject player;
    private float timer=0;
    // Start is called before the first frame update
    private void Start()
    {
        player = gameManager.player;
    }
    public void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < detectRange) {
            timer -= Time.fixedDeltaTime;
            if (timer < 0) {
                Drop();
                timer = spawnInterval;
            }
        } 
    }
    void Drop()
    {
        Vector3 dropPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + Random.Range(-spawnRange, spawnRange));
        gameManager manager = GameObject.Find("GameManager").GetComponent<gameManager>();
        if (Random.Range(0f, 1f) < healthAndShieldDropChance)
        {
            GameObject hs = Instantiate(drop, dropPosition, transform.rotation);
            pupCreater pup = hs.GetComponent<pupCreater>();
            if (Random.Range(0, 2) == 0)
            {
                pup.health = true;
                pup.healthAmount = manager.healthRecharge_;
            }
            else
            {
                pup.shield = true;
                pup.shieldAmount = manager.shieldRecharge_;
            }
            pup.Create();
        }
        float chance = Random.Range(0f, 1f);

        if (chance < WeaponDropChance)
        {
            GameObject hs = Instantiate(drop, dropPosition, transform.rotation);
            pupCreater pup = hs.GetComponent<pupCreater>();
            if (Random.Range(0f, 1f) < UsingWeaponDropChance)
            {
                    int Gunid = gameManager.player.GetComponentInChildren<GunPlaceHolderPlayer>().activePrimary;
                    pup.id = Gunid;
                    int type = Gunid % 10;
                    pup.ammo = manager.AmmoRechargePrimary[type];
            }
            else
            {
                int choice = Random.Range(0, 5);
                int variant = Random.Range(0, 3);
                int ammoFill = 0;
                int type = 0;
                int id = 0;
                if (choice != 0)
                {
                    type = Random.Range(0, 5);
                    id = 200 + variant * 10 + type;
                    ammoFill = manager.AmmoRechargePrimary[type];
                }
                else
                {
                    type = 0;
                    id = 100 + variant * 10 + type;
                    ammoFill = manager.ammorechargeSecondary[type];
                }
                pup.id = id;
                pup.ammo = ammoFill;
            }
            pup.Create();
        }
        chance = Random.Range(0f, 1f);
        if (chance < grenadeDropChance) {
            GameObject hs = Instantiate(drop, dropPosition, transform.rotation);
            pupCreater pup = hs.GetComponent<pupCreater>();
            int type = Random.Range(0, 3);
            int id = 300 + type;
            int ammoFill = manager.rechargeGrenades[type];
            pup.id = id;
            pup.ammo = ammoFill;
            pup.Create();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }
}
