
using UnityEngine;

public class DestroyableObject : MonoBehaviour, IKillable
{

    public void dead() {
        Debug.Log(gameObject.name+"dead");
        Destroy(gameObject);
    }
}
