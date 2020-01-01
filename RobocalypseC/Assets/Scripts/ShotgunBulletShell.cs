
using UnityEngine;

public class ShotgunBulletShell : Bullet
{
    public float PalletCount;
    public GameObject pallet;
    [Range(0, 100)]
    public float spread;
    public override void Start()
    {
        for (int i = 0; i < PalletCount; i++) {
            float deflect = Random.Range(spread, -spread);
            Vector3 shootDirection = new Vector3(transform.rotation.eulerAngles.x+deflect, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            GameObject bullet = Instantiate(pallet,transform.position, Quaternion.Euler(shootDirection));
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.damage = damage;
            bulletScript.healthDamageRatio = healthDamageRatio;
            bulletScript.ShieldDamageRatio = ShieldDamageRatio;
            bulletScript.FromPlayer = FromPlayer;
        }
        Destroy(gameObject);
    }

}
