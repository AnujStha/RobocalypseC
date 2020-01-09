using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDamage1 : MonoBehaviour
{
    public string detectTag;
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag(detectTag)) {
            enter();
        }
    }
    public virtual void enter() {

    }
}
