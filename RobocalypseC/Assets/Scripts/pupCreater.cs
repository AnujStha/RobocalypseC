using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pupCreater : MonoBehaviour
{
    public int ammo,id;
    public GameObject primaryCreate, SecondaryCreate;
    public void Create()
    {
        int type = id / 100;
        if (type == 2)
        {
            primary();
        }
        else {
            secondary();
        }
    }
    void primary() {
        PickupsPrimary pk= primaryCreate.GetComponent<PickupsPrimary>();
        pk.id = id;
        pk.ammo = ammo;
        Instantiate(primaryCreate, transform.position, transform.rotation);
    }
    void secondary() {
        PickupsSecondary pk = SecondaryCreate.GetComponent<PickupsSecondary>();
        pk.id = id;
        pk.ammo = ammo;
        Instantiate(SecondaryCreate, transform.position, transform.rotation);
    }
}
