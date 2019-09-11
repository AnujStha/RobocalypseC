
using UnityEngine;

public class DestroyableObject : MonoBehaviour, IKillable
{
    public GameObject destroyedObject;
    public void dead() {
        Debug.Log(gameObject.name+"dead");
        Instantiate(destroyedObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
