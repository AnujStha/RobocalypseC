using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlaceHolder : MonoBehaviour
{
    public Transform hand1;
    public Transform hand2;
    public GameObject ActiveGun;
    public bool Player;
    // Start is called before the first frame update
    public virtual void Start()
    {
        transform.position = new Vector3(0, (hand1.position.y + hand2.position.y) / 2, (hand1.position.z + hand2.position.z) / 2);
    }
    public virtual void LateUpdate()
    {
        transform.position = new Vector3(0, (hand1.position.y + hand2.position.y) / 2, (hand1.position.z + hand2.position.z) / 2);
        transform.LookAt(new Vector3(0, hand2.position.y, hand2.position.z));
       
    }

    public bool shoot() {
        ActiveGun.GetComponent<gun>().shoot(Player);
        return true;
    }
}
