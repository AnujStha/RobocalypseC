
using UnityEngine;

public class SmokeGrenade : Grenades
{
    public float smokeDuration;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }


    public override void explode()
    {
        isExploded = true;
        grenadeRB.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        GrenadeRenderer.SetActive(false);
        GameObject explosionEffect = Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Destroy(explosionEffect, smokeDuration);
        Destroy(gameObject,smokeDuration);
    }
}
