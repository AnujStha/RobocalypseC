
using UnityEngine;

public class touchDamage : MonoBehaviour
{
    public float damagePerSecond,shieldDamageRatio,HealthDamageRatio;
    HealthAndShield player;
    private void Start()
    {
        player = gameManager.player.GetComponent<HealthAndShield>();
    }
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("dasd");
        if (collision.gameObject.CompareTag("Player")) {
            player.damage(damagePerSecond * Time.fixedDeltaTime, HealthDamageRatio, shieldDamageRatio);
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("dasd");
        if (collision.gameObject.CompareTag("Player"))
        {
            player.damage(damagePerSecond * Time.fixedDeltaTime, HealthDamageRatio, shieldDamageRatio);
        }
    }
}
