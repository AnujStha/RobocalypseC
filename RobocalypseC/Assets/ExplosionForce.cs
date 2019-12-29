using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce : MonoBehaviour
{ public float force;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Rigidbody r in gameObject.GetComponentsInChildren<Rigidbody>()) {
            Vector3 direction = new Vector3(Random.Range(-1f, 1f) * force, Random.Range(-1f, 1f) * force, Random.Range(-1f, 1f)*force);
            r.AddForce(direction, ForceMode.Impulse);
            StartCoroutine(disable());
        } 
    }
    IEnumerator disable() {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < gameObject.transform.childCount; i++) {
            GameObject c = gameObject.transform.GetChild(i).gameObject;
            Rigidbody r = c.GetComponent<Rigidbody>();
            if (r != null) {
                r.isKinematic = true;
            }
            Collider co = c.GetComponent<Collider>();
            if (co != null) {
                co.enabled = false;
            }
        }
    }
}
